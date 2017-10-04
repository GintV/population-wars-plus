using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Attacks
{
    internal class DoubleArrow : IAttack
    {
        public int NumberOfProjectiles { get; }
        public int AttackSpeed { get; }
        
        public DoubleArrow()
        {
            NumberOfProjectiles = 2;
            AttackSpeed = 20;
        }

        public IProjectile CreateProjectile() =>
            new Arrow();
    }
}
