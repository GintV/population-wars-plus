namespace TowerDefense.Source.Guardians.Archers
{
    internal class LightArcher : Archer
    {
        public static Archer CreateArcher() =>
            new LightArcher();

        public override void Attack() { }

        public override void ActivateChargeAttack() { }

        public override void Promote() { }

        public override void Upgrade() { }
    }
}