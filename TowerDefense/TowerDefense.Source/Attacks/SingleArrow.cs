using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Attacks
{
    internal class SingleArrow : IAttack
    {
        public int NumberOfProjectiles { get; }
        public int AttackSpeed { get; }
        
        public SingleArrow()
        {
            NumberOfProjectiles = 1;
            AttackSpeed = 20;
        }

        public IProjectile CreateProjectile() =>
            new Arrow();
    }
}
