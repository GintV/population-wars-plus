using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Attacks.Projectiles
{
    class NullAttack : IAttack
    {
        public void SetLocation(Vector2 location) { }

        public List<IProjectile> Shoot(Vector2 target, int targetSpeed) => new List<IProjectile>();

        public void Upgrade() { }
    }
}
