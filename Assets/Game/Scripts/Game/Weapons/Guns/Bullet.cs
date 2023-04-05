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
        
        [SerializeField, HideInInspector] private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody.velocity = transform.forward * speed;
        }

        public void Hit()
        {
            if (canBeSplited)
            {
                SendBulletPart(45);
                SendBulletPart(-45);
            }
            
            Push();
        }

        private void SendBulletPart(float angle)
        {
            Bullet bulletPart = BulletsPool.Instance.Pop(bulletPartPrefab);

            bulletPart.transform.position = transform.position;
            
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
            bulletPart.transform.forward = direction;
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
