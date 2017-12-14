using System.Collections.Generic;
using System.Numerics;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Guardians;
using TowerDefense.Source.Guardians.States;

namespace TowerDefense.Source.Loggers
{
    public class WizardLogger : Guardian
    {
        private readonly string m_displayName;
        private readonly Guardian m_guardian;
        private readonly ILogger m_logger;

        public WizardLogger(string displayName, Guardian guardian, ILogger logger)
        {
            m_displayName = displayName;
            m_guardian = guardian;
            m_logger = logger;
        }

        public new List<Projectile> Attack(Vector2 target, int targetSpeed)
        {
            m_logger.Log($"Prepare to be crushed with my magic! I, {m_displayName}, shall destroy you!");
            var valueToReturn = m_guardian.Attack(target, targetSpeed);
            m_logger.Log("What did I told you!");
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
            m_logger.Log("Oh yeah, I can feel great power!");
            m_guardian.ActivateChargeAttack();
            m_logger.Log("Charge!");
        }

        public override void Promote()
        {
            m_logger.Log("Hell yeah! Would you like to be first to taste my new power?");
            m_guardian.Promote();
            m_logger.Log("I am ready!");
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
            m_guardian.ActivateChargeAttack();
            m_logger.Log("Level up!");
        }

        public override void Downgrade(AttackType oldAttackTypeType, int oldUpgradeCost)
        {
            m_logger.Log("Why...?");
            m_guardian.Downgrade(oldAttackTypeType, oldUpgradeCost);
        }
    }
}
