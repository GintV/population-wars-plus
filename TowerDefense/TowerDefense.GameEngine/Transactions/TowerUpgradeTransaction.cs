using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefense.GameEngine.Transactions
{
    internal class TowerUpgradeTransaction : CoinTransaction
    {
        public override bool Execute(IGameEnvironment gameplay)
        {
            CoinDifference = gameplay.Tower.UpgradeCost;
            if (gameplay.Inventory.Coins.Get() < CoinDifference) return false;
            gameplay.Inventory.Coins.Set(gameplay.Inventory.Coins.Get() - CoinDifference);
            gameplay.Tower.Upgrade();
            return true;
        }

        // TODO prevent from suiciding by implementing is undoable
        public override bool Undo(IGameEnvironment gameplay)
        {
            if (!IsUndoable()) return false;
            gameplay.Inventory.Coins.Set(gameplay.Inventory.Coins.Get() + CoinDifference);
            if (gameplay.Tower.Level % 20 == 0)
            {
                var guardianSpace = gameplay.Tower.GuardianSpace;
                var guardianToMove = guardianSpace.TowerBlocks[guardianSpace.Blocks - 1].Guardian;
                if (guardianToMove != null)
                {
                    gameplay.Inventory.Guardians.Add(guardianToMove);
                }
            }
            gameplay.Tower.Downgrade(CoinDifference);
            return true;
        }
    }
}
