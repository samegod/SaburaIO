using UnityEngine;

namespace Game.Interfaces
{
    public interface IHitable
    {
        void Hit(Transform hitPoint, float damage);
        void Slash(Transform hitPoint, float damage);
        void Shoot(Transform hitPoint, float damage);
    }
}