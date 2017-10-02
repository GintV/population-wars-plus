using System;

namespace TowerDefense.Source.Guardians.Archers
{
    internal abstract class Archer : IGuardian
    {
        public Guid Id { get; }

        protected Archer()
        {
            Id = Guid.NewGuid();
        }

        public abstract void Attack();
        public abstract void ActivateChargeAttack();
        public abstract void Promote();
        public abstract void Upgrade();
    }
}