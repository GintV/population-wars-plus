using System;
using System.Numerics;
using TowerDefense.Source.Attacks.Projectiles.MoveTypes;

namespace TowerDefense.Source.Attacks.Projectiles
{
    public interface IProjectile : ICloneable
    {
        void Move();
        void SetLocation(Vector2 location);
        void Upgrade();
    }

    public abstract class Projectile : IProjectile
    {
        public int CollisionDamage { get; protected set; }
        public Vector2 Location { get; set; } // TODO: to protected set
        public double Speed { get; protected set; }
        public abstract IMove MoveType { get; protected set; }

        protected Projectile(int collisionDamage, double speed)
        {
            CollisionDamage = collisionDamage;
            Speed = speed;
        }

        public abstract object Clone();
        public abstract void Upgrade();

        public void Move() => Location = MoveType.Move();
        public void SetLocation(Vector2 location) => Location = location;

    }
}