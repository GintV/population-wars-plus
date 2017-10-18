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
    }

    public abstract class Monster : IMonster
    {
        public Guid Id { get; }
        public abstract int HealthPoints { get; }
        public abstract int HealthPointsRemaining { get; }
        public abstract Vector2 Location { get; set; }
        public abstract int Speed { get; }

        protected Monster()
        {
            Id = Guid.NewGuid();
        }
    }
}