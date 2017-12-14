using System.Collections.Generic;
using System.Numerics;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Guardians.States
{
    public abstract class State
    {
        protected long InitialCounter { get; set; }
        protected long Counter { get; set; }

        protected State(long initialCounter)
        {
            InitialCounter = initialCounter;
            Counter = initialCounter;
        }

        public List<Projectile> Attack(Vector2 target, int targetSpeed, long dt, Guardian guardian)
        {
            Counter = Counter - dt;
            if (Counter < 0)
                ChangeState(guardian);
            return InitiateAttack(target, targetSpeed);
        }
        public abstract List<Projectile> InitiateAttack(Vector2 target, int targetSpeed);
        public abstract void ChangeState(Guardian guardian);
        public void Upgrade(int initialCounter) => InitialCounter = initialCounter;
    }
}