using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class Projectile : IMovable
    {
        private readonly float m_speed;
        private readonly IRenderable m_target;
        private readonly Game m_context;

        public Projectile(int speed, IRenderable target, Game context)
        {
            m_speed = speed;
            m_target = target;
            m_context = context;
        }

        public Dims Position { get; set; }
        public Dims Size { get; set; }
        public Image Image { get; set; }

        public void Move(long dTime)
        {
            var distMoved = m_speed * dTime / 100000;
            if (m_target == null)
            {
                Position.X += distMoved;
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
            Position.X += (float)(distMoved * ratioX);
            Position.Y += (float)(distMoved * ratioY);
        }
    }
}
