using Game.Interfaces;
using Game.Weapons;
using UnityEngine;

namespace Game.Character
{
    [RequireComponent(typeof(CharacterMovement))]
    public class Character : MonoBehaviour, IHitable
    {
        [SerializeField] private Weapon initialWeapon;
        [SerializeField] private float health;
        
        private IWeapon _currentWeapon;
        private CharacterHealth _health;
        private CharacterMovement _characterMovement;

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _health = new CharacterHealth
            {
                CurrentHealth = health,
                ThisCharacter = this
            };
        }

        private void Start()
        {
            SetupWeapon();
        }
        
        public void Attack()
        {
            _currentWeapon.Attack();
        }

        public void SetupWeapon()
        {
            _currentWeapon = initialWeapon;
        }
        
        public void Move(Vector3 axis)
        {
            _characterMovement.Move(axis);
        }

        public void Die()
        {
            Debug.Log("Character " + name + " died");
        }

        public void Hit(Transform hitPoint, float damage)
        {
            _health.TakeDamage(damage);
        }

        public void Slash(Transform hitPoint, float damage)
        {
            _health.TakeDamage(damage);
        }

        public void Shoot(Transform hitPoint, float damage)
        {
            _health.TakeDamage(damage);
        }
    }
}
