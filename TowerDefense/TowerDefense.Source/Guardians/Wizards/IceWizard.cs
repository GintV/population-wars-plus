namespace TowerDefense.Source.Guardians.Wizards
{
    internal class IceWizard : Wizard
    {
        public static Wizard CreateWizard() =>
            new IceWizard();

        public override void Attack() {}

        public override void ActivateChargeAttack() { }

        public override void Promote() { }

        public override void Upgrade() { }
    }
}
