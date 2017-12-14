using TowerDefense.Source.Attacks;
using TowerDefense.Source.Guardians;

namespace TowerDefense.GameEngine.Transactions
{
    internal class GuardianUpgrade : CoinTransaction
    {
        private AttackType _mPreviousAttackType;
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
            _mPreviousAttackType = UpgradedGuardian.AttackType;
            UpgradedGuardian.Upgrade();
            return true;
        }

        public override bool Undo(IGameEnvironment environment)
        {
            environment.Inventory.Coins.Set(environment.Inventory.Coins.Get() + CoinDifference);
            UpgradedGuardian.Downgrade(_mPreviousAttackType, CoinDifference);
            return true;
        }
    }
}
