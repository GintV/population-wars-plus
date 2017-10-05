using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.UI
{
    public class Enemy : IMovable
    {
        private readonly float m_speed;
        private readonly Game m_context;
        private readonly Vector2 m_target;
        public Enemy(int speed, Game context, Vector2 target)
        {
            m_speed = speed;
            m_context = context;
            m_target = target;
        }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Image Image { get; set; }

        public void Move(long dTime)
        {
            var dX = dTime * m_speed / 100000;
            Position = Vector2.Add(Position, new Vector2(-dX, 0));
            if (Position.X < m_target.X)
            {
                m_context.Destroy(this);
            }
        }
    }
}
