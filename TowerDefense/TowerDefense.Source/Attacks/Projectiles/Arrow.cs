using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Attacks.Projectiles
{
    internal class Arrow : IProjectile
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
    }
}
