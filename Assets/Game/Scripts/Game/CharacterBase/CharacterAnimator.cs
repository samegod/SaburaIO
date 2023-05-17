using Game.Weapons;
using UnityEngine;

namespace Game.CharacterBase
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int HorizontalHash = Animator.StringToHash("Horizontal");
        private static readonly int VerticalHash = Animator.StringToHash("Vertical");
        private static readonly int ReloadHash = Animator.StringToHash("Reload");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int SwordHash = Animator.StringToHash("Sword");
        private static readonly int PistolHash = Animator.StringToHash("Pistol");
        private static readonly int RifleHash = Animator.StringToHash("Rifle");
        private static readonly int DamageHash = Animator.StringToHash("Damage");
        private static readonly int DieHash = Animator.StringToHash("Death");

        public void Motion(Vector3 direction, float speedMultiplier)
        {
            Quaternion targetDirection = Quaternion.LookRotation(direction);
            float angle = transform.eulerAngles.y - targetDirection.eulerAngles.y;
            angle *= -1;
            Vector3 newDirection = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;

            newDirection *= speedMultiplier;
            
            animator.SetFloat(HorizontalHash, newDirection.x);
            animator.SetFloat(VerticalHash, newDirection.z);
        }

        public void PickWeapon(WeaponName weaponName)
        {
            switch (weaponName)
            {
                case WeaponName.Katana:
                    animator.SetBool(SwordHash, true);
                    break;
                case WeaponName.Pistol:
                    animator.SetBool(PistolHash, true);
                    break;
                case WeaponName.Rifle:
                    animator.SetBool(RifleHash, true);
                    break;
            }
        }

        public void Damage()
        {
            animator.SetTrigger(DamageHash);
        }
        
        public void Attack()
        {
            animator.SetTrigger(AttackHash);
        }
        
        public void Reload()
        {
            animator.SetTrigger(ReloadHash);
        }

        public void Die()
        {
            animator.SetTrigger(DieHash);
        }
    }
}
