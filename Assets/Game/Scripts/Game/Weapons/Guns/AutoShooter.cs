using System;
using UnityEngine;

namespace Game.Weapons.Guns
{
    public class AutoShooter : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private float shootRate;

        private float _timeCounter;
        
        private void Update()
        {
            _timeCounter += Time.deltaTime;

            if (_timeCounter >= shootRate)
            {
                weapon.Attack();
                _timeCounter = 0f;
            }
        }
    }
}
