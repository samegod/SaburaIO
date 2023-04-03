using UnityEngine;

namespace Game.Weapons.Guns
{
    public class Pistol : Weapons.Weapon
    {
        [SerializeField] private Bullet bullet;
        [SerializeField] private Transform shootPoint;
        
        public override void Attack()
        {
            Bullet newBullet = BulletsPool.Instance.Pop(bullet);
            newBullet.transform.position = shootPoint.position;
            newBullet.transform.rotation = shootPoint.rotation;
        }

        public override void Reload()
        {
            
        }
    }
}
