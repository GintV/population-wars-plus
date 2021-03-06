﻿using System;
using System.Numerics;
using TowerDefense.Source.Attacks.Projectiles.MoveTypes;
using TowerDefense.Source.Mediator;
using TowerDefense.Source.Monsters;

namespace TowerDefense.Source.Attacks.Projectiles
{
    public interface IProjectile : ICloneable
    {
        void Move(long dt);
        void SetLocation(Vector2 location);
        void Upgrade();
        void Damage(Skull skull);
        void Damage(Bubble bubble);
        void Downgrade();
    }

    public abstract class Projectile : Notifier, IProjectile
    {
        public int CollisionDamage { get; protected set; }
        public Vector2 Location { get; set; } // TODO: to protected set
        public double Speed { get; protected set; }
        public abstract IMove MoveType { get; set; }

        protected Projectile(int collisionDamage, double speed)
        {
            CollisionDamage = collisionDamage;
            Speed = speed;
        }

        public abstract object Clone();
        public abstract void Upgrade();
        public abstract void Damage(Skull skull);
        public abstract void Damage(Bubble bubble);
        public abstract void Downgrade();

        public void Move(long dt)
        {
            Location = MoveType.Move(Location, dt);
            Send(Location, CollisionDamage);
        }

        public void SetLocation(Vector2 location) => Location = location;

        public override void Receive(Vector2 location, int damage, Notifier sender)
        {
            var dif = (Location - location).Length();
            if (dif < 40)
            {
                Send(Location, CollisionDamage);
                Destroy();
            }
        }
    }
}