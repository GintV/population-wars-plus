using System.Linq;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;
using static TowerDefense.Source.Constants;

namespace TowerDefense.Source.Guardians.Wizards
{
    public class FireWizard : Guardian
    {
        public sealed override IAttack AttackType { get; protected set; }
        public sealed override int ChargeAttackCost { get; protected set; }
        public sealed override bool ChargeAttackEnabled { get; protected set; }
        public sealed override double ChargeAttackTimer { get; protected set; }
        public sealed override int PromoteCost { get; protected set; }
        public sealed override int PromoteLevel { get; protected set; }
        public sealed override int UpgradeCost { get; protected set; }

        public FireWizard()
        {
            // TODO: implement MageBall and others
            //AttackType = new SingleArrow(AttackSpeedBase.SingleMageBall, new MageBall(ProjectileDamageBase.MageBall, ProjectileSpeedBase.MageBall));
            ChargeAttackCost = ChargeAttackCostBase.FireWizard;
            ChargeAttackEnabled = false;
            ChargeAttackTimer = ChargeAttackTimerBase.FireWizard;
            PromoteCost = GuardianPromoteCostBase.FireWizard;
            PromoteLevel = GuardianPromotionLevels.FireWizard.First();
            UpgradeCost = GuardianUpgradeCostBase.FireWizard;

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

        public sealed override void Upgrade()
        {
            ++Level;
            UpgradeCost = (int)(UpgradeCost * GuardianUpgradeCostMultiplier.FireWizard);
            AttackType.Upgrade();
        }
    }
}
