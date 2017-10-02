namespace TowerDefense.Source.Guardians.Wizards
{
    internal class FireWizard : Wizard
    {
        public static Wizard CreateWizard() =>
            new FireWizard();

        public override void Attack() { }

        public override void ActivateChargeAttack() { }

        public override void Promote() { }

        public override void Upgrade() { }
    }
}
