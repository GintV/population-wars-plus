using System;
using System.Numerics;

namespace TowerDefense.Source.Attacks.Projectiles
{
    public interface IProjectile
    {
        Guid TargetId { get; set; }
        int ProjectileSpeed { get; }
        Vector2 Location { get; set; }

        bool Move(Vector2 targetLocation);
    }
}
