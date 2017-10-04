using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Attacks
{
    public interface IAttack
    {
        int NumberOfProjectiles { get; }
        int AttackSpeed { get; }

        IProjectile CreateProjectile();
    }
}
