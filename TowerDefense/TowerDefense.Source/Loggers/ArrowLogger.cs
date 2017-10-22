using System;
using System.Numerics;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Loggers
{
    public class ArrowLogger : IProjectile
    {
        private readonly string m_displayName;
        private readonly IProjectile m_projectile;
        private readonly ILogger m_logger;

        public ArrowLogger(string displayName, IProjectile projectile, ILogger logger)
        {
            m_displayName = displayName;
            m_projectile = projectile;
            m_logger = logger;
        }

        //public Guid TargetId { get => m_projectile.TargetId; set => m_projectile.TargetId = value; }

        //public int ProjectileSpeed => m_projectile.ProjectileSpeed;

        //public Vector2 Location { get => m_projectile.Location; set => m_projectile.Location = value; }

        /*public bool Move(Vector2 targetLocation)
        {
            m_logger.Log($"{m_displayName} is moving from {m_projectile.Location.ToString()} to {targetLocation.ToString()}");
            return m_projectile.Move(targetLocation);
        }*/

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void SetLocation(Vector2 location)
        {
            throw new NotImplementedException();
        }

        public void Upgrade()
        {
            throw new NotImplementedException();
        }
    }
}