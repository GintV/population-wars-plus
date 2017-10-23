using System;
using System.Collections.Generic;
using System.Numerics;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Guardians
{
    public interface IGuardian
    {
        List<IProjectile> Attack(Vector2 target, int targetSpeed);
        void ActivateChargeAttack();
        void Promote();
        void SetGuardianLocation(Vector2 location);
        void Demote(IAttack oldAttackType, int oldPromoteLevel);
        void Upgrade();
        void Downgrade(IAttack oldAttackType, int oldUpgradeCost);
    }

    public abstract class Guardian : IGuardian
    {
        public abstract IAttack AttackType { get; protected set; }
        public abstract int ChargeAttackCost { get; protected set; }
        public abstract bool ChargeAttackEnabled { get; protected set; }
        public abstract double ChargeAttackTimer { get; protected set; }
        public abstract int PromoteCost { get; protected set; }
        public abstract int PromoteLevel { get; protected set; }
        public abstract int UpgradeCost { get; protected set; }

        public Guid Id { get; }
        public int Level { get; protected set; }

        protected Guardian()
        {
            Id = Guid.NewGuid();
            Level = 0;
        }

        public abstract void ActivateChargeAttack();
        public abstract void Promote();
        public abstract void Demote(IAttack oldAttackType, int oldPromoteLevel);
        public abstract void Upgrade();
        public abstract void Downgrade(IAttack oldAttackType, int oldUpgradeCost);

        public List<IProjectile> Attack(Vector2 target, int targetSpeed) => AttackType.Shoot(target, targetSpeed);
        public void SetGuardianLocation(Vector2 location) => AttackType.SetLocation(location);
    }
}