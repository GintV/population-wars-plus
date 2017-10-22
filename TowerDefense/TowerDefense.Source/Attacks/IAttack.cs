using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Attacks.Projectiles.MoveTypes;

namespace TowerDefense.Source.Attacks
{
    public interface IAttack
    {
        void SetLocation(Vector2 location);
        List<IProjectile> Shoot(Vector2 target, int targetSpeed);
        void Upgrade();
    }

    public abstract class AttackType : IAttack
    {
        protected double AttackTimer { get; set; }
        protected double AttackSpeed { get; set; }
        protected IProjectile Projectile { get; set; }

        protected AttackType(double attackSpeed, IProjectile projectile)
        {
            AttackSpeed = attackSpeed;
            AttackTimer = 1.0 / attackSpeed;
            Projectile = projectile;
        }

        public abstract List<IProjectile> Shoot(Vector2 target, int targetSpeed);
        public abstract void Upgrade();

        public void SetLocation(Vector2 location) => Projectile.SetLocation(location);
    }
}
