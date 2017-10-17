﻿using TowerDefense.Source.Guardians;

namespace TowerDefense.Source.Loggers
{
    public class WizardLogger : IGuardian
    {
        private readonly string m_displayName;
        private readonly IGuardian m_guardian;
        private readonly ILogger m_logger;

        public WizardLogger(string displayName, IGuardian guardian, ILogger logger)
        {
            m_displayName = displayName;
            m_guardian = guardian;
            m_logger = logger;
        }

        public void Attack()
        {
            m_logger.Log($"Prepare to be crushed with my magic! I, {m_displayName}, shall destroy you!");
            m_guardian.Attack();
            m_logger.Log("What did I told you!");
        }

        public void ActivateChargeAttack()
        {
            m_logger.Log("Oh yeah, I can feel great power!");
            m_guardian.ActivateChargeAttack();
            m_logger.Log("Charge!");
        }

        public void Promote()
        {
            m_logger.Log("Hell yeah! Would you like to be first to taste my new power?");
            m_guardian.Promote();
            m_logger.Log("I am ready!");
        }

        public void Upgrade()
        {
            m_logger.Log("Wohoo!");
            m_guardian.ActivateChargeAttack();
            m_logger.Log("Level up!");
        }
    }
}
