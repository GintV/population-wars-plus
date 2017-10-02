using System;

namespace TowerDefense.Source.Guardians.Wizards
{
    internal abstract class Wizard : IGuardian
    {
        public Guid Id { get; }

        protected Wizard()
        {
            Id = Guid.NewGuid();
        }

        public abstract void Attack();
        public abstract void ActivateChargeAttack();
        public abstract void Promote();
        public abstract void Upgrade();
    }
}
