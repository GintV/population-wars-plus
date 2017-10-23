using TowerDefense.Source.Attacks;
using TowerDefense.Source.Guardians;

namespace TowerDefense.GameEngine.Transactions
{
    internal class GuardianUpgrade : CoinTransaction
    {
        private IAttack m_previousAttack;
        public Guardian UpgradedGuardian { get; }

        public GuardianUpgrade(Guardian guardian)
        {
            UpgradedGuardian = guardian;
        }

        public override bool Execute(IGameEnvironment gameplay)
        {
            CoinDifference += UpgradedGuardian.UpgradeCost;
            gameplay.Inventory.Coins.Set(gameplay.Inventory.Coins.Get() - CoinDifference);
            // TODO deep copy
            m_previousAttack = UpgradedGuardian.AttackType;
            UpgradedGuardian.Upgrade();
            return true;
        }

        public override bool Undo(IGameEnvironment gameplay)
        {
            gameplay.Inventory.Coins.Set(gameplay.Inventory.Coins.Get() + CoinDifference);
            UpgradedGuardian.Downgrade(m_previousAttack, CoinDifference);
            return true;
        }
    }
}
