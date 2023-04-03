using Game.Weapons;
using UnityEngine;

namespace Game.Character
{
    [RequireComponent(typeof(CharacterMovement))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private Weapon initialWeapon;
        
        private IWeapon _currentWeapon;
        
        private CharacterMovement _characterMovement;

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
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
    }
}
