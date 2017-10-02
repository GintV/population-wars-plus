using System;

namespace TowerDefense.Source.Guardians.Archers
{
    internal class DarkArcher : Archer
    {
        public static Archer CreateArcher() =>
            new DarkArcher();

        public override void Attack() { }

        public override void ActivateChargeAttack() { }

        public override void Promote() { }

        public override void Upgrade() { }
    }
}