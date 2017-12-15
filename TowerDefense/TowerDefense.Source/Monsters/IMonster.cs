using System;
using System.Numerics;
using TowerDefense.Source.Mediator;

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

    public abstract class Monster : Notifier, IMonster
    {
        public Guid Id { get; }
        public abstract int HealthPoints { get; }
        public abstract int HealthPointsRemaining { get; set; }
        public Vector2 Location { get; set; }
        public abstract int Speed { get; }
        public abstract void Move(long dt);

        protected Monster()
        {
            Id = Guid.NewGuid();
        }

        public override void Receive(Vector2 location, int damage, Notifier sender)
        {
            var dif = (Location - location).Length();
            if (dif < 40)
            {
                HealthPointsRemaining -= damage;
                sender.Destroy();
            }
            if(HealthPointsRemaining < 0)
                Destroy();
        }
    }
}