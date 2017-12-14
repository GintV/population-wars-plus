using System;
using System.Numerics;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Attacks.Projectiles.MoveTypes;

namespace TowerDefense.Source.Loggers
{
    public class ArrowLogger : IProjectile
    {
        private readonly string m_displayName;
        private readonly Projectile m_projectile;
        private readonly ILogger m_logger;

        public ArrowLogger(string displayName, Projectile projectile, ILogger logger)
        {
            m_displayName = displayName;
            m_projectile = projectile;
            m_logger = logger;
        }

        //public Guid TargetId { get => m_projectile.TargetId; set => m_projectile.TargetId = value; }

        //public int ProjectileSpeed => m_projectile.ProjectileSpeed;

        //public Vector2 Location { get => m_projectile.Location; set => m_projectile.Location = value; }

        public void Move(long dt)
        {
            m_logger.Log($"{m_displayName} is moving from {m_projectile.Location.ToString()} to {((MoveType)m_projectile.MoveType).Target.ToString()}");
            m_projectile.Move(dt);
        }

        public object Clone()
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