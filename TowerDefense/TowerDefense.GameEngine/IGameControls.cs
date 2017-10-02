using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense.Source;
using TowerDefense.Source.Guardians;
using static System.Guid;

namespace TowerDefense.GameEngine
{
    public interface IGameControls
    {
        void ActivateChargeAttack(int guardianSlot);
        void CreateGuardian(GuardianType guardianType, int guardianSlot);
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
        protected List<IGuardian> Guardians { get; }
        protected GuardianSpace GuardianSpace { get; }

        public GameControls()
        {
            GameStarted = Gameplay.GetGameplay().GameStarted;
            Guardians = Gameplay.GetGameplay().Inventory.Guardians;
            GuardianSpace = Gameplay.GetGameplay().Tower.GuardianSpace;
        }

        public void ActivateChargeAttack(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if(guardian.HasValue)
                guardian.Value.ActivateChargeAttack();
        }

        public void CreateGuardian(GuardianType guardianType, int guardianSlot)
        {
            if (guardianSlot > GuardianSpace.Slots - 1)
                return;
            var guardian = GuardianFactory.CreateGuardian(GuardianTypeConverter.Convert(guardianType));
            if (!guardian.HasValue)
                return;
            Gameplay.GetGameplay().Inventory.Guardians.Add(guardian.Value);
            GuardianSpace.GuardianSlots[guardianSlot].Guardian = guardian.Value;
        }

        public void PromoteGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (guardian.HasValue)
                guardian.Value.Promote();
        }
        public void StartGame() => Gameplay.GetGameplay().RunGame();

        public void SwitchToNextGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (!guardian.HasValue)
                return;
            GuardianSpace.GuardianSlots[guardianSlot].Guardian = Guardians.SkipWhile(g => g.Id != guardian.Value.Id).Skip(1).FirstOrDefault() ??
                Guardians.First();
        }

        public void SwitchToPreviousGuardian(int guardianSlot)
        {
            var guardian = GetGuardian(guardianSlot);
            if (!guardian.HasValue)
                return;
            GuardianSpace.GuardianSlots[guardianSlot].Guardian = Guardians.TakeWhile(g => g.Id != guardian.Value.Id).LastOrDefault() ??
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
            Gameplay.GetGameplay().Tower.Upgrade();
        }

        private Maybe<IGuardian> GetGuardian(int guardianSlot)
        {
            if (!GameStarted)
                return null;
            if (guardianSlot > GuardianSpace.Slots - 1)
                return null;
            return new Maybe<IGuardian>(GuardianSpace.GuardianSlots[guardianSlot].Guardian);
        }
    }
}
