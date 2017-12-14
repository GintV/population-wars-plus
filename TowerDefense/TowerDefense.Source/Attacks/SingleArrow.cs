using System.Collections.Generic;
using System.Numerics;
using TowerDefense.Source.Attacks.Projectiles;
using static TowerDefense.Source.Constants.GameEngineSettings;

namespace TowerDefense.Source.Attacks
{
    internal class SingleArrow : AttackType
    {
        public SingleArrow(double attackSpeed, Projectile projectile) : base(attackSpeed, projectile) { }

        public override List<Projectile> Shoot(Vector2 target, int targetSpeed)
        {
            AttackTimer -= GameCycleInSeconds;
            if (AttackTimer > 0)
                return new List<Projectile>();
            AttackTimer = 1.0 / AttackSpeed;
            var projectile = (Projectile)Projectile.Clone();
            projectile.MoveType.Initialize(projectile.Location, projectile.Speed, target, targetSpeed);
            return new List<Projectile> { projectile };
        }
        public override void Upgrade()
        {
            AttackSpeed = (int)(AttackSpeed * Constants.AttackSpeedMultiplier.SingleArrow);
            Projectile.Upgrade();
        }
    }
}
