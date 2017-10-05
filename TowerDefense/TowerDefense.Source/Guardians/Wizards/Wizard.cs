using System;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Guardians.Wizards
{
    public abstract class Wizard : IGuardian
    {
        public Guid Id { get; }
        public int AttackPower { get; set; }

        public IAttack AttackType { get; }

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
