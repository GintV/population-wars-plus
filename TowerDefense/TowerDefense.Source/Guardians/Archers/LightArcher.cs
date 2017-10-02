namespace TowerDefense.Source.Guardians.Archers
{
    internal class LightArcher : Archer
    {
        public static Archer CreateArcher() => new LightArcher();

        public override void BaseAttack() { }

        public override void ChargeAttack() { }

        public override void Promote() { }

        public override void Upgrade() { }
    }
}