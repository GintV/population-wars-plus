namespace TowerDefense.Source.Guardians.Wizards
{
    internal class FireWizard : Wizard
    {
        public static Wizard CreateWizard() => new FireWizard();

        public override void BaseAttack() { }

        public override void ChargeAttack() { }

        public override void Promote() { }

        public override void Upgrade() { }
    }
}
