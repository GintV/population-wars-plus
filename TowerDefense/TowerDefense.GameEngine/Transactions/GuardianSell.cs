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

        public override bool Execute(IGameEnvironment gameplay)
        {
            for (int i = 0; i < gameplay.Tower.GuardianSpace.Blocks; i++)
            {
                if (gameplay.Tower.GuardianSpace.TowerBlocks[i].Guardian.Equals(m_soldGuardian))
                {
                    gameplay.Tower.GuardianSpace.TowerBlocks[i].Guardian = null;
                    m_blockIndex = i;
                    // TODO moneys returning goes here
                    return true;
                }
            }
            return false;
        }

        public override bool Undo(IGameEnvironment gameplay)
        {
            if (gameplay.Inventory.Coins.Get() < CoinDifference) return false;
            gameplay.Inventory.Coins.Set(gameplay.Inventory.Coins.Get() + CoinDifference);
            if (gameplay.Tower.GuardianSpace.TowerBlocks[m_blockIndex].Guardian == null)
            {
                gameplay.Tower.GuardianSpace.TowerBlocks[m_blockIndex].Guardian = m_soldGuardian;
                return true;
            }

            for (int i = 0; i < gameplay.Tower.GuardianSpace.Blocks; i++)
            {
                if (gameplay.Tower.GuardianSpace.TowerBlocks[i].Guardian == null)
                {
                    gameplay.Tower.GuardianSpace.TowerBlocks[i].Guardian = m_soldGuardian;
                    return true;
                }
            }
            gameplay.Inventory.Guardians.Add(m_soldGuardian);
            return true;
        }
    }
}
