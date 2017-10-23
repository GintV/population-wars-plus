using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefense.GameEngine.Transactions
{
    internal class TowerUpgradeTransaction : CoinTransaction
    {
        public override bool Execute(IGameEnvironment environment)
        {
            CoinDifference = environment.Tower.UpgradeCost;
            if (environment.Inventory.Coins.Get() < CoinDifference) return false;
            environment.Inventory.Coins.Set(environment.Inventory.Coins.Get() - CoinDifference);
            environment.Tower.Upgrade();
            return true;
        }

        // TODO prevent from suiciding by implementing is undoable
        public override bool Undo(IGameEnvironment environment)
        {
            if (!IsUndoable()) return false;
            environment.Inventory.Coins.Set(environment.Inventory.Coins.Get() + CoinDifference);
            if (environment.Tower.Level % 20 == 0)
            {
                var guardianSpace = environment.Tower.GuardianSpace;
                var guardianToMove = guardianSpace.TowerBlocks[guardianSpace.Blocks - 1].Guardian;
                if (guardianToMove != null)
                {
                    environment.Inventory.Guardians.Add(guardianToMove);
                }
            }
            environment.Tower.Downgrade(CoinDifference);
            return true;
        }
    }
}
