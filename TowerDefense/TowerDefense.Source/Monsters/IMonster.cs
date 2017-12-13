using System;
using System.Numerics;

namespace TowerDefense.Source.Monsters
{
    public interface IMonster
    {
        Guid Id { get; }
        int HealthPoints { get; }
        int HealthPointsRemaining { get; }
        Vector2 Location { get; set; }

        int Speed { get; }

        void Move(long dt);
    }

    public abstract class Monster : IMonster
    {
        public Guid Id { get; }
        public abstract int HealthPoints { get; }
        public abstract int HealthPointsRemaining { get; }
        public Vector2 Location { get; set; }
        public abstract int Speed { get; }
        public abstract void Move(long dt);

        protected Monster()
        {
            Id = Guid.NewGuid();
        }
    }
}