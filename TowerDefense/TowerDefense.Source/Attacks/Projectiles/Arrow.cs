using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Attacks.Projectiles
{
    public class Arrow : IProjectile, ISpawnable
    {
        public Guid TargetId { get; set; }
        public int ProjectileSpeed { get; }
        public Vector2 Location { get; set; }

        public Arrow()
        {
            ProjectileSpeed = 50;
        }

        public bool Move(Vector2 targetLocation)
        {
            var trajectory = targetLocation - Location;
            var distance = Math.Abs(trajectory.Length());
            if (distance < ProjectileSpeed)
            {
                Location = targetLocation;
                return true;
            }
            Location += Vector2.Normalize(trajectory) * ProjectileSpeed;
            return false;
        }

        public object Spawn() => (Arrow)this.MemberwiseClone();
    }
}
