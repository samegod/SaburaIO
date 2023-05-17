using System;
using System.Collections;
using Game.CharacterBase;
using UnityEngine;

namespace Game.Weapons
{
    public enum WeaponType
    {
        Melee, 
        Firearm,
    }

    public enum WeaponName
    {
        Katana,
        Pistol,
        Rifle,
    }
    
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public event Action OnAttack;
        public event Action OnReloadStart;
        public event Action OnReloadFinished;
        
        [SerializeField] private WeaponType type;
        [SerializeField] private WeaponName weaponName;
        [SerializeField] private Transform graphics;
        [SerializeField] private float attackDelay;
        [SerializeField] private float reloadTime;
        [SerializeField] private bool useAmmo;
        [Header("Ammo")]
        [SerializeField] private int maxAmmo;
        [SerializeField] private int maxMag;
        [SerializeField] private int currentAmmo;
        [SerializeField] private int currentMag;

        [SerializeField] protected bool CanAttack;
        [SerializeField] protected bool Reloading;
        [SerializeField] protected bool GotAmmo;

        public WeaponName Name => weaponName;

        private void Start()
        {
            if (!useAmmo)
            {
                GotAmmo = true;
            }

            CanAttack = true;
            Reloading = false;
                
            CheckAmmo();
        }

        public void Attack()
        { 
            if (CanAttack && !Reloading && GotAmmo)
            {
                AttackLogic();
                CanAttack = false;
                StartCoroutine(AttackDelay());
                DecreaseAmmo();
            }
        }

        public void SetRoot(CharacterRigPoints rigPoints)
        {
            switch (type)
            {
                case WeaponType.Melee:
                    graphics.parent = rigPoints.MeleeRightHand;
                    break;
                case WeaponType.Firearm:
                    graphics.parent = rigPoints.FirearmRightHand;
                    break;
            }
            
            graphics.localPosition = Vector3.zero;
        }
        
        protected void DecreaseAmmo()
        {
            if (!useAmmo)
                return;

            currentMag--;
            
            CheckAmmo();
            
            if (currentMag <= 0 && currentAmmo > 0)
            {
                StartCoroutine(ReloadProcess());
            }
        }

        private void CheckAmmo()
        {
            if (!useAmmo)
                return;
            
            if (currentMag > 0)
            {
                GotAmmo = true;
            }
            else if (currentMag <= 0)
            {
                GotAmmo = false;
            }
        }

        public abstract void AttackLogic();

        public abstract void ReloadLogic();
        public bool GetCanAttack()
        {
            return CanAttack && !Reloading && GotAmmo;
        }

        protected IEnumerator ReloadProcess()
        {
            Reloading = true;
            ReloadLogic();
            yield return new WaitForSeconds(reloadTime);
            Reloading = false;

            if (currentAmmo >= maxMag)
            {
                currentMag = maxMag;
                currentAmmo -= maxMag;
            }
            else
            {
                currentMag = currentAmmo;
                currentAmmo = 0;
            }
            CheckAmmo();
        }

        protected IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(attackDelay);
            CanAttack = true;
        }
    }
}