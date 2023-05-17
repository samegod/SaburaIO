using Additions.Pool;
using Game.Interfaces;
using UnityEngine;

namespace Game.Weapons.Guns
{
    public class Bullet : MonoBehaviourPoolObject, IHitable
    {
        [SerializeField] private bool canBeSplited = true;
        [SerializeField] private float speed;
        [SerializeField] private Bullet bulletPartPrefab;
        
        private float _damage;

        [SerializeField, HideInInspector] private Rigidbody _rigidbody;

        public void Hit(Transform hitPoint, float damage)
        {
        }

        public void Slash(Transform hitPoint, float damage)
        {
            bool turnLeft = true;
            Vector3 newDirection = transform.InverseTransformPoint(hitPoint.position);
            var angle = Quaternion.LookRotation(newDirection).eulerAngles.y;
            
            if (angle > 180)
            {
                turnLeft = false;
                angle -= 180;
            }

            if (angle > 90)
            {
                angle = 180 - angle;
            }

            angle /= 2f;

            if (turnLeft)
            {
                angle *= -1;
            }

            if (canBeSplited)
            {
                SendBulletPart(angle + 30f);
                SendBulletPart(angle - 30f);
            }

            Push();
        }

        public void Shoot(Transform hitPoint, float damage)
        {
        }

        public void SendBullet(float damage)
        {
            _rigidbody.velocity = transform.forward * speed;
            _damage = damage;
        }

        private void SendBulletPart(float angle)
        {
            Bullet bulletPart = BulletsPool.Instance.Pop(bulletPartPrefab);

            bulletPart.transform.position = transform.position;

            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
            bulletPart.transform.forward = direction;

            bulletPart.SendBullet(_damage);
        }

        public override void Push()
        {
            BulletsPool.Instance.Push(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            IHitable target = other.GetComponent<IHitable>();

            if (target != null)
            {
                if (target != this)
                {
                    target.Shoot(transform, _damage);
                }
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
#endif
    }
}