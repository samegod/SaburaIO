using UnityEngine;

namespace Game.Scripts.Game.Weapon
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public abstract void Attack();

        public abstract void Reload();
    }
}
