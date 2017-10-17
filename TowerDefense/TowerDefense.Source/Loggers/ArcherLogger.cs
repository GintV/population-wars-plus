using TowerDefense.Source.Guardians;

namespace TowerDefense.Source.Loggers
{
    public class ArcherLogger : IGuardian
    {
        private readonly string m_displayName;
        private readonly IGuardian m_guardian;
        private readonly ILogger m_logger;

        public ArcherLogger(string displayName, IGuardian guardian, ILogger logger)
        {
            m_displayName = displayName;
            m_guardian = guardian;
            m_logger = logger;
        }

        public void Attack()
        {
            m_logger.Log($"My mighty arrows shall pierce your body! The best {m_displayName} is here!");
            m_guardian.Attack();
            m_logger.Log("Sharp, sharp, sharp...");
        }

        public void ActivateChargeAttack()
        {
            m_logger.Log("Let's have some fun!");
            m_guardian.ActivateChargeAttack();
            m_logger.Log("Easy!");
        }

        public void Promote()
        {
            m_logger.Log("More power? Why not.");
            m_guardian.Promote();
            m_logger.Log("Better than expected!");
        }

        public void Upgrade()
        {
            m_logger.Log("Wohoo!");
            m_guardian.ActivateChargeAttack();
            m_logger.Log("Level up!");
        }
    }
}