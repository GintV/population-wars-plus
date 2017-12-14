using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDefense.Source.Guardians;

namespace TowerDefense.GameEngine.Transactions
{
    internal class GuardianSell : CoinTransaction
    {
        private readonly Guardian m_soldGuardian;
        private int m_blockIndex;

        public GuardianSell(Guardian guardianToSell)
        {
            m_soldGuardian = guardianToSell;
        }

        public override bool Execute(IGameEnvironment environment)
        {
            for (int i = 0; i < environment.Tower.GuardianSpace.Blocks; i++)
            {
                if (environment.Tower.GuardianSpace.TowerBlocks[i].Guardian != null && environment.Tower.GuardianSpace.TowerBlocks[i].Guardian.Equals(m_soldGuardian))
                {
                    environment.Inventory.Guardians.Remove(environment.Tower.GuardianSpace.TowerBlocks[i].Guardian);
                    environment.Tower.GuardianSpace.TowerBlocks[i].Guardian = null;
                    m_blockIndex = i;
                    // TODO moneys returning goes here
                    return true;
                }
            }
            return false;
        }

        public override bool Undo(IGameEnvironment environment)
        {
            if (environment.Inventory.Coins.Get() < CoinDifference) return false;
            environment.Inventory.Coins.Set(environment.Inventory.Coins.Get() + CoinDifference);
            if (environment.Tower.GuardianSpace.TowerBlocks[m_blockIndex].Guardian == null)
            {
                environment.Tower.GuardianSpace.TowerBlocks[m_blockIndex].Guardian = m_soldGuardian;
                return true;
            }

            for (int i = 0; i < environment.Tower.GuardianSpace.Blocks; i++)
            {
                if (environment.Tower.GuardianSpace.TowerBlocks[i].Guardian == null)
                {
                    environment.Tower.GuardianSpace.TowerBlocks[i].Guardian = m_soldGuardian;
                    return true;
                }
            }
            environment.Inventory.Guardians.Add(m_soldGuardian);
            return true;
        }
    }
}
