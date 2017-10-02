namespace TowerDefense.Source.Guardians.Archers
{
    internal class DarkArcher : Archer
    {
        public static Archer CreateArcher() => new DarkArcher();

        public override void BaseAttack() { }

        public override void ChargeAttack() { }

        public override void Promote() { }

        public override void Upgrade() { }
    }
}