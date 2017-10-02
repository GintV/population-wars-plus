using System;

namespace TowerDefense.GameEngine
{
    public interface IGameControls
    {
        void ActivateChargeAttack(int guardianSlot);
        void CreateGuardian(GuardianType guardianType);
        void PromoteGuardian(int guardianSlot);
        void StartGame();
        void SwitchToNextGuardian(int guardianSlot);
        void SwitchToPreviousGuardian(int guardianSlot);
        void UpgradeGuardian(int guardianSlot);
        void UpgradeTower();
    }
}
