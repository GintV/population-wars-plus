using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Monsters
{
    public class NullMonster : Monster
    {
        public override int HealthPoints { get; }
        public override int HealthPointsRemaining { get; set; }
        public override int Speed { get; }

        public NullMonster() { }

        public override void Move(long dt) { }
        public override void TakeDamage(IProjectile projectile) { }
    }
}
