using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles.MoveTypes;

namespace TowerDefense.Source.Attacks.Projectiles
{
    public class Arrow : IProjectile, ISpawnable
    {
        public Guid TargetId { get; set; }
        public int ProjectileSpeed { get; }
        public Vector2 Location { get; set; }
        public IMoveType MoveType { get; set; }

        public Arrow()
        {
            ProjectileSpeed = 10;
            MoveType = new LineMove();
        }

        public bool Move(Vector2 targetLocation)
        {
            var loc = Location;
            bool returns = MoveType.Move(ref loc, targetLocation: targetLocation, speed: ProjectileSpeed);
            Location = loc;
            return returns;
        }

        public object Spawn() => (Arrow)this.MemberwiseClone();
    }
}
