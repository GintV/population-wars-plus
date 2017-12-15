using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Attacks.Projectiles.MoveTypes;

namespace TowerDefense.Source.Attacks
{
    public abstract class AttackType
    {
        public double AttackTimer { get; set; }
        protected double AttackSpeed { get; set; }
        protected IProjectile Projectile { get; set; }

        protected AttackType(double attackSpeed, IProjectile projectile)
        {
            AttackSpeed = attackSpeed;
            AttackTimer = 1.0 / attackSpeed;
            Projectile = projectile;
        }

        public abstract List<Projectile> Shoot(Vector2 target, int targetSpeed);
        public abstract void Upgrade();
        public abstract void Downgrade();

        public void SetLocation(Vector2 location) => Projectile.SetLocation(location);
    }
}
