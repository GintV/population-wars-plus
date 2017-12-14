using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Attacks.Projectiles
{
    class NullAttack : AttackType
    {
        public NullAttack() : base(0, new Arrow()) { }

        public new void SetLocation(Vector2 location) { }

        public override List<Projectile> Shoot(Vector2 target, int targetSpeed) => new List<Projectile>();

        public override void Upgrade() { }
    }
}
