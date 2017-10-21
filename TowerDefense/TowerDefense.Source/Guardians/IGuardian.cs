using System;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Guardians
{
    public interface IGuardian
    {
        void Attack();
        void ActivateChargeAttack();
        void Promote();
        void Upgrade();
    }

    public abstract class Guardian : IGuardian
    {
        public Guid Id { get; }
        public int AttackPower { get; set; }
        public IAttack AttackType { get; }
        public int Level { get; }
        public int PromoteCost { get; }
        public int PromoteLevel { get; }
        public int UpgradeCost { get; }

        protected Guardian()
        {
            Id = Guid.NewGuid();
        }

        public abstract void Attack();
        public abstract void ActivateChargeAttack();
        public abstract void Promote();
        public abstract void Upgrade();
    }
}