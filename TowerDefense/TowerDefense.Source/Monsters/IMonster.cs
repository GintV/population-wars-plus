using System;
using System.Numerics;

namespace TowerDefense.Source.Monsters
{
    public interface IMonster
    {
        Guid Id { get; }
        int HealthPoints { get; }
        int HealthPointsRemaining { get; }
        Vector2 Location { get; }

        int Speed { get; }
    }

    internal abstract class Monster : IMonster
    {
        public Guid Id { get; }
        public abstract int HealthPoints { get; }
        public abstract int HealthPointsRemaining { get; }
        public abstract Vector2 Location { get; }
        public abstract int Speed { get; }

        protected Monster()
        {
            Id = Guid.NewGuid();
        }
    }
}