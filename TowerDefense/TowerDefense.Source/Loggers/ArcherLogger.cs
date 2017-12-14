using System.Collections.Generic;
using System.Numerics;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Guardians;
using TowerDefense.Source.Guardians.States;

namespace TowerDefense.Source.Loggers
{
    public class ArcherLogger : Guardian
    {
        private readonly string m_displayName;
        private readonly Guardian m_guardian;
        private readonly ILogger m_logger;

        public ArcherLogger(string displayName, Guardian guardian, ILogger logger)
        {
            m_displayName = displayName;
            m_guardian = guardian;
            m_logger = logger;
        }

        public new List<Projectile> Attack(Vector2 target, int targetSpeed)
        {
            m_logger.Log($"My mighty arrows shall pierce your body! The best {m_displayName} is here!");
            var valueToReturn = m_guardian.Attack(target, targetSpeed);
            m_logger.Log("Sharp, sharp, sharp...");
            return valueToReturn;
        }

        public override AttackType AttackType { get; protected set; }
        public override int ChargeAttackCost { get; protected set; }
        public override bool ChargeAttackEnabled { get; protected set; }
        public override double ChargeAttackTimer { get; protected set; }
        public override int PromoteCost { get; protected set; }
        public override int PromoteLevel { get; protected set; }
        public override int UpgradeCost { get; protected set; }
        public override State GuardianState { get; set; }

        public override void ActivateChargeAttack()
        {
            m_logger.Log("Let's have some fun!");
            m_guardian.ActivateChargeAttack();
            m_logger.Log("Easy!");
        }

        public override void Promote()
        {
            m_logger.Log("More power? Why not.");
            m_guardian.Promote();
            m_logger.Log("Better than expected!");
        }

        public override void Demote(AttackType oldAttackTypeType, int oldPromoteLevel)
        {
            m_logger.Log(":(");
            m_guardian.Demote(oldAttackTypeType, oldPromoteLevel);
        }

        public new void SetGuardianLocation(Vector2 location)
        {
            throw new System.NotImplementedException();
        }

        public override void Upgrade()
        {
            m_logger.Log("Wohoo!");
            m_guardian.Upgrade();
            m_logger.Log("Level up!");
        }

        public override void Downgrade(AttackType oldAttackTypeType, int oldUpgradeCost)
        {
            m_logger.Log("Why...?");
            m_guardian.Downgrade(oldAttackTypeType, oldUpgradeCost);
        }
    }
}