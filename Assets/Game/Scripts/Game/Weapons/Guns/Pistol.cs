using UnityEngine;

namespace Game.Weapons.Guns
{
    public class Pistol : Weapon
    {
        [SerializeField] private Bullet bullet;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float damage;
        
        public override void Attack()
        {
            Bullet newBullet = BulletsPool.Instance.Pop(bullet);
            newBullet.transform.position = shootPoint.position;
            newBullet.transform.rotation = shootPoint.rotation;
            newBullet.Shoot(damage);
        }

        public override void Reload()
        {
            
        }
    }
}
