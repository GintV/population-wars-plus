using System;
using System.Collections.Generic;
using System.Numerics;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Guardians.States;

namespace TowerDefense.Source.Guardians
{
    public abstract class Guardian
    {
        public abstract AttackType AttackType { get; protected set; }
        public abstract int ChargeAttackCost { get; protected set; }
        public abstract bool ChargeAttackEnabled { get; protected set; }
        public abstract double ChargeAttackTimer { get; protected set; }
        public abstract int PromoteCost { get; protected set; }
        public abstract int PromoteLevel { get; protected set; }
        public abstract int UpgradeCost { get; protected set; }
        public abstract State GuardianState { get; set; }

        public Guid Id { get; }
        public int Level { get; protected set; }

        protected Guardian()
        {
            Id = Guid.NewGuid();
            Level = 0;
        }

        public abstract void ActivateChargeAttack();
        public abstract void Promote();
        public abstract void Demote(AttackType oldAttackTypeType, int oldPromoteLevel);
        public abstract void Upgrade();
        public abstract void Downgrade(AttackType oldAttackTypeType, int oldUpgradeCost);

        public List<Projectile> Attack(Vector2 target, int targetSpeed) => GuardianState.Attack(target, targetSpeed, this);
        public void SetGuardianLocation(Vector2 location) => AttackType.SetLocation(location);
    }
}