namespace TowerDefense.Source.Guardians.Archers
{
    internal abstract class Archer : IGuardian
    {
        public abstract void BaseAttack();
        public abstract void ChargeAttack();
        public abstract void Promote();
        public abstract void Upgrade();
    }
}