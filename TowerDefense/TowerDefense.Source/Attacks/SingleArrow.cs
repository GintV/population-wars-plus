using System.Collections.Generic;
using System.Numerics;
using TowerDefense.Source.Attacks.Projectiles;
using static TowerDefense.Source.Constants.GameEngineSettings;

namespace TowerDefense.Source.Attacks
{
    internal class SingleArrow : AttackType
    {
        public SingleArrow(double attackSpeed, IProjectile projectile) : base(attackSpeed, projectile) { }

        public override List<IProjectile> Shoot(Vector2 target, int targetSpeed)
        {
            AttackTimer -= GameCycleInSeconds;
            if (AttackTimer > 0)
                return new List<IProjectile>();
            AttackTimer = 1.0 / AttackSpeed;
            var projectile = (Projectile)Projectile.Clone();
            projectile.MoveType.Initialize(projectile.Location, projectile.Speed, target, targetSpeed);
            return new List<IProjectile> { projectile };
        }
        public override void Upgrade()
        {
            AttackSpeed = (int)(AttackSpeed * Constants.AttackSpeedMultiplier.SingleArrow);
            Projectile.Upgrade();
        }
    }
}
