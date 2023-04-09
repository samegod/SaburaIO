using Additions.Pool;
using Game.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Weapons.Guns
{
    public class Bullet : MonoBehaviourPoolObject, IHitable
    {
        [SerializeField] private bool canBeSplited = true;
        [SerializeField] private float speed;
        [SerializeField] private Bullet bulletPartPrefab;

        [SerializeField, HideInInspector] private Rigidbody _rigidbody;

        public void Hit(Transform hitPoint)
        {
        }

        public void Slash(Transform hitPoint)
        {
            Vector3 newDirection = transform.InverseTransformPoint(hitPoint.position);
            var angle = Quaternion.LookRotation(newDirection).eulerAngles.y;

            
            if (angle > 180)
            {
                angle -= 180;
            }

            if (angle > 90)
            {
                angle = 180 - angle;
            }

            angle /= 2f;

            if (canBeSplited)
            {
                SendBulletPart(angle + 30f);
                SendBulletPart(angle - 30f);
            }

            Push();
        }

        public void Shoot(Transform hitPoint)
        {
            throw new System.NotImplementedException();
        }

        public void Shoot()
        {
            _rigidbody.velocity = transform.forward * speed;
            
            Debug.Log(transform.eulerAngles.y);
        }

        public void Hit()
        {
        }

        private void SendBulletPart(float angle)
        {
            Bullet bulletPart = BulletsPool.Instance.Pop(bulletPartPrefab);

            bulletPart.transform.position = transform.position;

            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
            bulletPart.transform.forward = direction;

            bulletPart.Shoot();
        }

        public override void Push()
        {
            BulletsPool.Instance.Push(this);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
#endif
    }
}