using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense.Source;
using TowerDefense.Source.Guardians;
using static TowerDefense.GameEngine.Gameplay;

namespace TowerDefense.GameEngine
{
    public interface IGameControls
    {
        void ActivateChargeAttack(int guardianSlot);
        void CreateGuardian(string guardianClass, string guardianType, int guardianSlot);
        void PromoteGuardian(int guardianSlot);
        void StartGame();
        void SwitchToNextGuardian(int guardianSlot);
        void SwitchToPreviousGuardian(int guardianSlot);
        void UpgradeGuardian(int guardianSlot);
        void UpgradeTower();
    }

    internal class GameControls : IGameControls
    {
        protected Boolean GameStarted { get; }
        protected List<Guardian> Guardians { get; }
        protected GuardianSpace GuardianSpace { get; }

        public GameControls()
        {
            GameStarted = GetGameplay().GameStarted;
            Guardians = GetGameplay().Inventory.Guardians;
            GuardianSpace = GetGameplay().Tower.GuardianSpace;
        }

        public void ActivateChargeAttack(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if(guardian.HasValue)
                guardian.Value.ActivateChargeAttack();
        }

        public void CreateGuardian(string guardianClass, string guardianType, int guardianSlot)
        {
            if (guardianSlot > GuardianSpace.Blocks - 1)
                return;
            (var enumClass, var enumType) = GuardianTypeConverter.Convert(guardianClass, guardianType);
            var factory = GuardianFactoryProducer.GetFactory(enumClass);
            if (!factory.HasValue)
                return;
            var guardian = factory.Value.CreateGuardian(enumType);
            if (!guardian.HasValue)
                return;
            GetGameplay().Inventory.Guardians.Add(guardian.Value);
            GuardianSpace.TowerBlocks[guardianSlot].Guardian = guardian.Value;
        }

        public void PromoteGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (guardian.HasValue)
                guardian.Value.Promote();
        }
        public void StartGame() => GetGameplay().RunGame();

        public void SwitchToNextGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (!guardian.HasValue)
                return;
            GuardianSpace.TowerBlocks[guardianSlot].Guardian = Guardians.SkipWhile(g => g.Id != guardian.Value.Id).Skip(1).FirstOrDefault() ??
                Guardians.First();
        }

        public void SwitchToPreviousGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (!guardian.HasValue)
                return;
            GuardianSpace.TowerBlocks[guardianSlot].Guardian = Guardians.TakeWhile(g => g.Id != guardian.Value.Id).LastOrDefault() ??
                Guardians.Last();
        }

        public void UpgradeGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if(guardian.HasValue)
                guardian.Value.Upgrade();
        }

        public void UpgradeTower()
        {
            GetGameplay().Tower.Upgrade();
        }

        private Maybe<Guardian> GetGuardian(int guardianSlot)
        {
            if (!GameStarted)
                return null;
            if (guardianSlot > GuardianSpace.Blocks - 1)
                return null;
            return GuardianSpace.TowerBlocks[guardianSlot].Guardian;
        }
    }
}
