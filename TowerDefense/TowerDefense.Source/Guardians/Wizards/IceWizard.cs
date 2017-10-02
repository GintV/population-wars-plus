namespace TowerDefense.Source.Guardians.Wizards
{
    internal class IceWizard : Wizard
    {
        public static Wizard CreateWizard() => new IceWizard();

        public override void BaseAttack() {}

        public override void ChargeAttack() { }

        public override void Promote() { }

        public override void Upgrade() { }
    }
}
