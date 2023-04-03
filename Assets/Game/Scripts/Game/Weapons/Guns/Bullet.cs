using Additions.Pool;
using UnityEngine;

namespace Game.Weapons.Guns
{
    public class Bullet : MonoBehaviourPoolObject
    {
        [SerializeField] private float speed;
        
        [SerializeField, HideInInspector] private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody.velocity = transform.forward * speed;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
#endif
        public override void Push()
        {
            
        }
    }
}
