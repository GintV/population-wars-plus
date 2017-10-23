using System.Collections.Generic;
using System.Linq;
using TowerDefense.GameEngine.Transactions;
using TowerDefense.Source;
using TowerDefense.Source.Guardians;
using static TowerDefense.Source.Constants.ConfigurationSettings;

namespace TowerDefense.GameEngine
{
    public interface IGameControls
    {
        void ActivateChargeAttack(int guardianSlot);
        void CreateGuardian(string guardianClass, string guardianType);
        void MoveGuardianToInventory(int guardianSlot);
        void PromoteGuardian(int guardianSlot);
        void SellGuardian(int guardianSlot);
        void SwapGuardians(int guardianSlot, int inventoryIndex);
        void SwitchToNextGuardian(int guardianSlot);
        void SwitchToPreviousGuardian(int guardianSlot);
        void UndoLastPurchase();
        void UpgradeGuardian(int guardianSlot);
        void UpgradeTower();
    }

    internal class GameControls : IGameControls
    {
        protected ICoinTransactionController CoinTransactionController { get; }
        protected IConfiguration Configuration { get; }
        protected IGameEnvironment GameEnvironment { get; }
        protected List<Guardian> Guardians { get; }
        protected GuardianSpace GuardianSpace { get; }

        public GameControls(ICoinTransactionController coinTransactionController, IConfiguration configuration, IGameEnvironment gameEnvironment)
        {
            CoinTransactionController = coinTransactionController;
            Configuration = configuration;
            GameEnvironment = gameEnvironment;
            Guardians = gameEnvironment.Inventory.Guardians;
            GuardianSpace = gameEnvironment.Tower.GuardianSpace;
            CoinTransactionController = coinTransactionController;
        }

        public void ActivateChargeAttack(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (guardian.HasValue)
                guardian.Value.ActivateChargeAttack();
        }

        public void CreateGuardian(string guardianClass, string guardianType)
        {
            (var enumClass, var enumType) = GuardianTypeConverter.Convert(guardianClass, guardianType);
            CoinTransactionController.AddTransaction(new GuardianBuy(enumClass, enumType, BaseGuardianCreationCost));
        }

        public void MoveGuardianToInventory(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (!guardian.HasValue) return;
            GuardianSpace.TowerBlocks[guardianSlot].Guardian = null;
        }

        public void PromoteGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (guardian.HasValue && guardian.Value.PromoteLevel > guardian.Value.Level)
                CoinTransactionController.AddTransaction(new GuardianPromote(guardian.Value));
        }

        public void SellGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (guardian.HasValue)
                CoinTransactionController.AddTransaction(new GuardianSell(guardian.Value));
        }

        public void SwapGuardians(int guardianSlot, int inventoryIndex)
        {
            if (inventoryIndex >= Guardians.Count) return;
            var inventoryGuardian = Guardians.Except(GuardianSpace.TowerBlocks.Select(b => b.Guardian)).ElementAt(inventoryIndex);
            GuardianSpace.TowerBlocks[guardianSlot].Guardian = inventoryGuardian;
        }

        public void SwitchToNextGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (!guardian.HasValue)
                return;
            var towerGuardians = GuardianSpace.TowerBlocks.Select(towerBlock => towerBlock.Guardian?.Id).
                Where(id => id != null && id != guardian.Value.Id);
            GuardianSpace.TowerBlocks[guardianSlot].Guardian = Guardians.Where(g => !towerGuardians.Contains(g?.Id)).
                SkipWhile(g => g?.Id != guardian.Value.Id).Skip(1).FirstOrDefault() ?? Guardians.First();
        }

        public void SwitchToPreviousGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (!guardian.HasValue)
                return;
            var towerGuardians = GuardianSpace.TowerBlocks.Select(towerBlock => towerBlock.Guardian?.Id).
                Where(id => id != null && id != guardian.Value.Id);
            GuardianSpace.TowerBlocks[guardianSlot].Guardian = Guardians.Where(g => !towerGuardians.Contains(g?.Id)).
                TakeWhile(g => g.Id != guardian.Value.Id).LastOrDefault() ?? Guardians.Last();
        }

        public void UndoLastPurchase() => CoinTransactionController.UndoLastTransaction();

        public void UpgradeGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (guardian.HasValue)
                CoinTransactionController.AddTransaction(new GuardianUpgrade(guardian.Value));
        }

        public void UpgradeTower() => CoinTransactionController.AddTransaction(new TowerUpgradeTransaction());

        private Maybe<Guardian> GetGuardian(int guardianSlot) => guardianSlot > GuardianSpace.Blocks - 1 ? null :
            GuardianSpace.TowerBlocks[guardianSlot].Guardian;
    }
}
