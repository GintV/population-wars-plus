using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Attacks
{
    class SingleMageBall : AttackType
    {
        public SingleMageBall(double attackSpeed, Projectile projectile) : base(attackSpeed, projectile) { }

        public override List<Projectile> Shoot(Vector2 target, int targetSpeed)
        {
            AttackTimer = 1.0 / AttackSpeed;
            var projectile = (Projectile)Projectile.Clone();
            projectile.MoveType.Initialize(projectile.Location, projectile.Speed, target, targetSpeed);
            return new List<Projectile> { projectile };
        }
        public override void Upgrade()
        {
            AttackSpeed = AttackSpeed * Constants.AttackSpeedMultiplier.SingleMageBall;
            Projectile.Upgrade();
        }
    }
}
