using TowerDefense.Source.Attacks;
using TowerDefense.Source.Guardians;

namespace TowerDefense.GameEngine.Transactions
{
    internal class GuardianUpgrade : CoinTransaction
    {
        private IAttack m_previousAttack;
        public Guardian UpgradedGuardian { get; }

        public GuardianUpgrade(Guardian guardianToUpgrade)
        {
            UpgradedGuardian = guardianToUpgrade;
        }

        public override bool Execute(IGameEnvironment environment)
        {
            CoinDifference += UpgradedGuardian.UpgradeCost;
            environment.Inventory.Coins.Set(environment.Inventory.Coins.Get() - CoinDifference);
            // TODO deep copy
            m_previousAttack = UpgradedGuardian.AttackType;
            UpgradedGuardian.Upgrade();
            return true;
        }

        public override bool Undo(IGameEnvironment environment)
        {
            environment.Inventory.Coins.Set(environment.Inventory.Coins.Get() + CoinDifference);
            UpgradedGuardian.Downgrade(m_previousAttack, CoinDifference);
            return true;
        }
    }
}
