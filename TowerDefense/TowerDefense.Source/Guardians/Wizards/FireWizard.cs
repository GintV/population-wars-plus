using System.Linq;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Guardians.States;
using static TowerDefense.Source.Constants;

namespace TowerDefense.Source.Guardians.Wizards
{
    public class FireWizard : Guardian
    {
        public sealed override AttackType AttackType { get; protected set; }
        public sealed override int ChargeAttackCost { get; protected set; }
        public sealed override bool ChargeAttackEnabled { get; protected set; }
        public sealed override double ChargeAttackTimer { get; protected set; }
        public sealed override int PromoteCost { get; protected set; }
        public sealed override int PromoteLevel { get; protected set; }
        public sealed override int UpgradeCost { get; protected set; }
        public sealed override State GuardianState { get; set; }

        public FireWizard()
        {
            AttackType = new SingleMageBall(AttackSpeedBase.SingleMageBall, new MageBall());
            ChargeAttackCost = ChargeAttackCostBase.FireWizard;
            ChargeAttackEnabled = false;
            ChargeAttackTimer = ChargeAttackTimerBase.FireWizard;
            PromoteCost = GuardianPromoteCostBase.FireWizard;
            PromoteLevel = GuardianPromotionLevels.FireWizard.First();
            UpgradeCost = GuardianUpgradeCostBase.FireWizard;
            GuardianState = new LoadingState((int)(AttackType.AttackTimer * 1000));
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
            UpgradeCost = (int)(UpgradeCost * GuardianUpgradeCostMultiplier.FireWizard);
            AttackType.Upgrade();
            GuardianState.Upgrade((int)(AttackType.AttackTimer * 1000));
        }
        public override void Downgrade(AttackType oldAttackTypeType, int oldUpgradeCost) { }
    }
}
