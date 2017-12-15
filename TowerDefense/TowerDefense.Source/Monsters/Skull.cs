using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Monsters
{
    public class Skull : Monster
    {
        private readonly float m_baseLine;
        public override int HealthPoints { get; }
        public override int HealthPointsRemaining { get; set; }
        public override int Speed { get; }
        public Skull(int speed, Vector2 location, int health)
        {
            Speed = speed;
            Location = location;
            HealthPoints = HealthPointsRemaining = health;
            m_baseLine = Location.Y;
        }

        public override void Move(long dt)
        {
            Location = new Vector2(Location.X - (int)(dt * Speed / 1000.0), (float)(m_baseLine + Math.Cos(Location.X / 25 - Math.PI / 2) * 30));
            Send(Location, 0);
        }
    }
}
