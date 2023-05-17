using Game.CombatSystem;
using Game.Interfaces;
using Game.Weapons;
using UnityEngine;

namespace Game.CharacterBase
{
    [RequireComponent(typeof(CharacterMovement))]
    public class Character : MonoBehaviour, IHitable
    {
        [SerializeField] private Weapon initialWeapon;
        [SerializeField] private float health;
        [SerializeField] private CharacterAnimator animator;
        [SerializeField] private CharacterRigPoints rigPoints;
        
        private IWeapon _currentWeapon;
        private CharacterHealth _health;
        private CharacterMovement _characterMovement;
        private bool _alive = true;

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
            if (!_alive)
                return;

            if (_currentWeapon.GetCanAttack())
            {
                _currentWeapon.Attack();
                animator.Attack();
            }
        }

        public void SetupWeapon()
        {
            if (!_alive)
                return;
            
            _currentWeapon = initialWeapon;
            initialWeapon.SetRoot(rigPoints);
            animator.PickWeapon(initialWeapon.Name);
        }
        
        public void Move(Vector3 axis, float speed)
        {
            if (!_alive)
                return;
            
            _characterMovement.Move(axis, speed);
            animator.Motion(axis, speed);
        }

        public void Die()
        {
            if (!_alive)
                return;
            
            _alive = false;
            animator.Die();
        }

        public void GetHit(float damage)
        {
            if (!_alive)
                return;
            
            _health.TakeDamage(damage);
            animator.Damage();
        }

        public void Hit(Transform hitPoint, float damage)
        {
            GetHit(damage);
        }

        public void Slash(Transform hitPoint, float damage)
        {
            GetHit(damage);
        }

        public void Shoot(Transform hitPoint, float damage)
        {
            GetHit(damage);
        }
    }
}
