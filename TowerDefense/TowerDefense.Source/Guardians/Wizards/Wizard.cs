namespace TowerDefense.Source.Guardians.Wizards
{
    internal abstract class Wizard : IGuardian
    {
        public abstract void BaseAttack();
        public abstract void ChargeAttack();
        public abstract void Promote();
        public abstract void Upgrade();
    }
}
