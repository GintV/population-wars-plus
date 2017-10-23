using System.Collections.Generic;
using System.Numerics;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;
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

        public List<IProjectile> Attack(Vector2 target, int targetSpeed)
        {
            m_logger.Log($"My mighty arrows shall pierce your body! The best {m_displayName} is here!");
            var valueToReturn = m_guardian.Attack(target, targetSpeed);
            m_logger.Log("Sharp, sharp, sharp...");
            return valueToReturn;
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

        public void Demote(IAttack oldAttackType, int oldPromoteLevel)
        {
            m_logger.Log(":(");
            m_guardian.Demote(oldAttackType, oldPromoteLevel);
        }

        public void SetGuardianLocation(Vector2 location)
        {
            throw new System.NotImplementedException();
        }

        public void Upgrade()
        {
            m_logger.Log("Wohoo!");
            m_guardian.Upgrade();
            m_logger.Log("Level up!");
        }

        public void Downgrade(IAttack oldAttackType, int oldUpgradeCost)
        {
            m_logger.Log("Why...?");
            m_guardian.Downgrade(oldAttackType, oldUpgradeCost);
        }
    }
}