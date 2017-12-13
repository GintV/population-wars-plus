using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Monsters
{
    public class Bubble : Monster
    {
        private readonly float m_baseLine;

        public override int HealthPoints { get; }
        public override int HealthPointsRemaining { get; }
        public override int Speed { get; }

        public Bubble(int speed, Vector2 location, int health)
        {
            Speed = speed;
            Location = location;
            HealthPoints = HealthPointsRemaining = health;
            m_baseLine = Location.Y;
        }

        public override void Move(long dt)
        {
            Location = new Vector2(Location.X - dt * Speed / 5, (float) (m_baseLine + Math.Sin(Location.X / 20) * 15));
        }
    }
}
