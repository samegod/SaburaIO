using UnityEngine;

namespace Game.Interfaces
{
    public interface IHitable
    {
        void Hit(Transform hitPoint);
        void Slash(Transform hitPoint);
        void Shoot(Transform hitPoint);
    }
}