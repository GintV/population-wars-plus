using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class Enemy : IMovable
    {
        private readonly float m_speed;
        private readonly Game m_context;
        private readonly Dims m_target;
        public Enemy(int speed, Game context, Dims target)
        {
            m_speed = speed;
            m_context = context;
            m_target = target;
        }
        public Dims Position { get; set; }
        public Dims Size { get; set; }
        public Image Image { get; set; }

        public void Move(long dTime)
        {
            var dX = dTime * m_speed / 100000;
            Position.X -= dX;
            if (Position.X < m_target.X)
            {
                m_context.Destroy(this);
            }
        }
    }
}
