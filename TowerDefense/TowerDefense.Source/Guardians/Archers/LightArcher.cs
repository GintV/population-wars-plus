using System.Linq;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Guardians.States;
using static TowerDefense.Source.Constants;

namespace TowerDefense.Source.Guardians.Archers
{
    public class LightArcher : Guardian
    {
        public sealed override AttackType AttackType { get; protected set; }
        public sealed override int ChargeAttackCost { get; protected set; }
        public sealed override bool ChargeAttackEnabled { get; protected set; }
        public sealed override double ChargeAttackTimer { get; protected set; }
        public sealed override int PromoteCost { get; protected set; }
        public sealed override int PromoteLevel { get; protected set; }
        public sealed override int UpgradeCost { get; protected set; }
        public sealed override State GuardianState { get; set; }

        public LightArcher()
        {
            AttackType = new SingleArrow(AttackSpeedBase.SingleArrow, new Arrow(ProjectileDamageBase.Arrow, ProjectileSpeedBase.Arrow));
            ChargeAttackCost = ChargeAttackCostBase.LightArcher;
            ChargeAttackEnabled = false;
            ChargeAttackTimer = ChargeAttackTimerBase.LightArcher;
            PromoteCost = GuardianPromoteCostBase.LightArcher;
            PromoteLevel = GuardianPromotionLevels.LightArcher.First();
            UpgradeCost = GuardianUpgradeCostBase.LightArcher;
            GuardianState = new LoadingState((int)(AttackType.AttackTimer * GameEngineSettings.GameCyclesPerSecond));
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
            UpgradeCost = (int)(UpgradeCost * GuardianUpgradeCostMultiplier.LightArcher);
            AttackType.Upgrade();
            GuardianState.Upgrade((int)(AttackType.AttackTimer * GameEngineSettings.GameCyclesPerSecond));
        }
        public override void Downgrade(AttackType oldAttackTypeType, int oldUpgradeCost) { }
    }
}