using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense.GameEngine.Transactions;
using TowerDefense.Source;
using TowerDefense.Source.Guardians;

namespace TowerDefense.GameEngine
{
    public interface IGameControls
    {
        void ActivateChargeAttack(int guardianSlot);
        void CreateGuardian(string guardianClass, string guardianType);
        void PromoteGuardian(int guardianSlot);
        void SwitchToNextGuardian(int guardianSlot);
        void SwitchToPreviousGuardian(int guardianSlot);
        void MoveGuardianToInventory(int guardianSlot);
        void SellGuardian(int guardianSlot);
        void SwapGuardians(int guardianSlot, int inventoryIndex);
        void UpgradeGuardian(int guardianSlot);
        void UpgradeTower();
        void UndoLastPurchase();
    }

    internal class GameControls : IGameControls
    {
        protected IConfiguration Configuration { get; }
        protected IGameEnvironment GameEnvironment { get; }
        protected List<Guardian> Guardians { get; }
        protected GuardianSpace GuardianSpace { get; }

        public GameControls(IConfiguration configuration, IGameEnvironment gameEnvironment)
        {
            Configuration = configuration;
            GameEnvironment = gameEnvironment;
            Guardians = gameEnvironment.Inventory.Guardians;
            GuardianSpace = gameEnvironment.Tower.GuardianSpace;
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
            GameEnvironment.TransactionController.AddTransaction(new GuardianBuy(enumClass, enumType, Configuration.BaseGuardianCreationCost));
        }

        public void PromoteGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (guardian.HasValue && guardian.Value.PromoteLevel > guardian.Value.Level)
                GameEnvironment.TransactionController.AddTransaction(new GuardianPromote(guardian.Value));
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
        public void UpgradeGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (guardian.HasValue)
                GameEnvironment.TransactionController.AddTransaction(new GuardianUpgrade(guardian.Value));
        }

        public void UpgradeTower() => GameEnvironment.TransactionController.AddTransaction(new TowerUpgradeTransaction());
        public void UndoLastPurchase() => GameEnvironment.TransactionController.UndoLastTransaction();

        public void MoveGuardianToInventory(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (!guardian.HasValue) return;
            GuardianSpace.TowerBlocks[guardianSlot].Guardian = null;
        }

        public void SellGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (guardian.HasValue)
                GameEnvironment.TransactionController.AddTransaction(new GuardianSell(guardian.Value));
        }

        public void SwapGuardians(int guardianSlot, int inventoryIndex)
        {
            if (inventoryIndex >= Guardians.Count) return;
            var inventoryGuardian = Guardians.Except(GuardianSpace.TowerBlocks.Select(b => b.Guardian)).ElementAt(inventoryIndex);
            GuardianSpace.TowerBlocks[guardianSlot].Guardian = inventoryGuardian;
        }

        private Maybe<Guardian> GetGuardian(int guardianSlot) => guardianSlot > GuardianSpace.Blocks - 1 ? null :
            GuardianSpace.TowerBlocks[guardianSlot].Guardian;
    }
}
