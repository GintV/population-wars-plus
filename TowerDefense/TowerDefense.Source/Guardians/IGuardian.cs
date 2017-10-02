namespace TowerDefense.Source.Guardians
{
    internal interface IGuardian
    {
        void BaseAttack();
        void ChargeAttack();
        void Promote();
        void Upgrade();
    }
}