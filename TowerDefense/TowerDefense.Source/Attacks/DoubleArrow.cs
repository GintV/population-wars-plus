using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles;
using static TowerDefense.Source.Constants.ConfigurationSettings;
using static TowerDefense.Source.Constants.GameEngineSettings;

namespace TowerDefense.Source.Attacks
{
    internal class DoubleArrow : SingleArrow
    {
        public DoubleArrow(double attackSpeed, Projectile projectile) : base(attackSpeed, projectile) { }

        public override List<Projectile> Shoot(Vector2 target, int targetSpeed)
        {
            //AttackTimer -= GameCycleInSeconds;
            //if (AttackTimer > 0)
            //    return new List<Projectile>();
            AttackTimer = 1.0 / AttackSpeed;
            (var projectileA, var projectileB) = ((Projectile) Projectile.Clone(), (Projectile) Projectile.Clone());
            projectileA.MoveType.Initialize(projectileA.Location, projectileA.Speed, target, targetSpeed);
            target.X += (float) (DistanceToTower / 100.0);
            projectileB.MoveType.Initialize(projectileB.Location, projectileB.Speed, target, targetSpeed);
            return new List<Projectile> { projectileA, projectileB };
        }
        public override void Upgrade()
        {
            AttackSpeed = (int)(AttackSpeed * Constants.AttackSpeedMultiplier.DoubleArrow);
            Projectile.Upgrade();
        }
    }
}
