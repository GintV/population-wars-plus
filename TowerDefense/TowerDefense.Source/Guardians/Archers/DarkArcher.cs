using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Guardians.States;
using static TowerDefense.Source.Constants;

namespace TowerDefense.Source.Guardians.Archers
{
    public class DarkArcher : Guardian
    {
        public sealed override AttackType AttackType { get; protected set; }
        public sealed override int ChargeAttackCost { get; protected set; }
        public sealed override bool ChargeAttackEnabled { get; protected set; }
        public sealed override double ChargeAttackTimer { get; protected set; }
        public sealed override int PromoteCost { get; protected set; }
        public sealed override int PromoteLevel { get; protected set; }
        public sealed override int UpgradeCost { get; protected set; }
        public override State GuardianState { get; set; }

        public DarkArcher()
        {
            AttackType = new SingleArrow(AttackSpeedBase.SingleArrow, new Arrow());
            ChargeAttackCost = ChargeAttackCostBase.DarkArcher;
            ChargeAttackEnabled = false;
            ChargeAttackTimer = ChargeAttackTimerBase.DarkArcher;
            PromoteCost = GuardianPromoteCostBase.DarkArcher;
            PromoteLevel = GuardianPromotionLevels.DarkArcher.First();
            UpgradeCost = GuardianUpgradeCostBase.DarkArcher;

            Upgrade();
        }

        public sealed override void ActivateChargeAttack()
        {
            // TODO: implement
        }

        public sealed override void Promote()
        {
            // TODO: implement
        }
        public override void Demote(AttackType oldAttackTypeType, int oldPromoteLevel) { }

        public sealed override void Upgrade()
        {
            ++Level;
            UpgradeCost = (int)(UpgradeCost * GuardianUpgradeCostMultiplier.DarkArcher);
            AttackType.Upgrade();
        }
        public override void Downgrade(AttackType oldAttackTypeType, int oldUpgradeCost) { }
    }
}