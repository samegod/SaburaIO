namespace Game.Weapons
{
    public interface IWeapon
    {
        void Attack();
        void AttackLogic();
        void ReloadLogic();
        bool GetCanAttack();
    }
}
