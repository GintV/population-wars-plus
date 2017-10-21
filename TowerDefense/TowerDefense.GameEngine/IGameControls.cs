using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense.Source;
using TowerDefense.Source.Guardians;

namespace TowerDefense.GameEngine
{
    public interface IGameControls
    {
        void ActivateChargeAttack(int guardianSlot);
        void CreateGuardian(string guardianClass, string guardianType, int guardianSlot);
        void PromoteGuardian(int guardianSlot);
        void SwitchToNextGuardian(int guardianSlot);
        void SwitchToPreviousGuardian(int guardianSlot);
        void UpgradeGuardian(int guardianSlot);
        void UpgradeTower();
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

        public void CreateGuardian(string guardianClass, string guardianType, int guardianSlot)
        {
            if (Configuration.BaseGuardianCreationCost > GameEnvironment.Inventory.Coins)
                return;
            if (guardianSlot > GuardianSpace.Blocks - 1)
                return;
            (var enumClass, var enumType) = GuardianTypeConverter.Convert(guardianClass, guardianType);
            var factory = GuardianFactoryProvider.GetFactory(enumClass);
            if (!factory.HasValue)
                return;
            var guardian = factory.Value.CreateGuardian(enumType);
            if (!guardian.HasValue)
                return;
            GameEnvironment.Inventory.Guardians.Add(guardian.Value);
            GuardianSpace.TowerBlocks[guardianSlot].Guardian = guardian.Value;
        }

        public void PromoteGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (!guardian.HasValue)
                return;
            var cost = guardian.Value.PromoteCost;
            if (cost > GameEnvironment.Inventory.Coins || guardian.Value.PromoteLevel > guardian.Value.Level)
                return;
            GameEnvironment.Inventory.Coins -= cost;
            guardian.Value.Promote();
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
            if (!guardian.HasValue)
                return;
            var cost = guardian.Value.UpgradeCost;
            if (cost > GameEnvironment.Inventory.Coins)
                return;
            GameEnvironment.Inventory.Coins -= cost;
            guardian.Value.Upgrade();
        }

        public void UpgradeTower()
        {
            var cost = GameEnvironment.Tower.UpgradeCost;
            if (cost > GameEnvironment.Inventory.Coins)
                return;
            GameEnvironment.Inventory.Coins -= cost;
            GameEnvironment.Tower.Upgrade();
        }

        private Maybe<Guardian> GetGuardian(int guardianSlot) => guardianSlot > GuardianSpace.Blocks - 1 ? null :
            GuardianSpace.TowerBlocks[guardianSlot].Guardian;
    }
}
