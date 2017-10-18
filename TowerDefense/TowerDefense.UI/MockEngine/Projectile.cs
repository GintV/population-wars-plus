using System;
using System.Drawing;
using System.Numerics;

namespace TowerDefense.UI.MockEngine
{
    public class Projectile : IMovable
    {
        private readonly float m_speed;
        private readonly IRenderable m_target;
        private readonly Game m_context;

        public Projectile(float speed, IRenderable target, Game context)
        {
            m_speed = speed;
            m_target = target;
            m_context = context;
        }

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Image Image { get; set; }

        public void Move(long dTime)
        {
            var distMoved = m_speed * dTime;
            if (m_target == null)
            {
                Position = Vector2.Add(Position, new Vector2(distMoved, 0));
                if (Position.X > m_context.Boundries.X)
                {
                    m_context.Destroy(this);
                }
                return;
            }
            var targetCoords = m_target.Position;
            var dX = targetCoords.X - Position.X;
            var dY = targetCoords.Y - Position.Y;
            var distLeft = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));
            if (distMoved > distLeft)
            {
                m_context.Destroy(m_target);
                m_context.Destroy(this);
            }
            var ratioX = dX / distLeft;
            var ratioY = dY / distLeft;
            Position = Vector2.Add(Position, new Vector2((float)(distMoved * ratioX), (float)(distMoved * ratioY)));
        }
    }
}
