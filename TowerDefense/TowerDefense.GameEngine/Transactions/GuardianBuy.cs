using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDefense.Source;
using TowerDefense.Source.Guardians;

namespace TowerDefense.GameEngine.Transactions
{
    internal class GuardianBuy : CoinTransaction
    {
        private IGuardian m_boughtGuardian;

        public Flags.GuardianClass GuardianClass { get; }
        public Flags.GuardianType GuardianType { get; }

        public GuardianBuy(Flags.GuardianClass guardianClass, Flags.GuardianType guardianType, int cost)
        {
            GuardianClass = guardianClass;
            GuardianType = guardianType;
            CoinDifference = cost;
        }

        public override bool Execute(IGameEnvironment gameplay)
        {
            if (gameplay.Inventory.Coins.Get() < CoinDifference) return false;
            gameplay.Inventory.Coins.Set(gameplay.Inventory.Coins.Get() - CoinDifference);
            var factory = GuardianFactoryProvider.GetFactory(GuardianClass);
            if (!factory.HasValue)
                return false;
            var guardian = factory.Value.CreateGuardian(GuardianType);
            if (!guardian.HasValue)
                return false;
            m_boughtGuardian = guardian.Value;
            gameplay.Inventory.Guardians.Add(guardian.Value);
            return true;
        }

        public override bool Undo(IGameEnvironment gameplay)
        {
            var found = true;
            if (!gameplay.Inventory.Guardians.Remove((Guardian)m_boughtGuardian))
            {
                found = false;
                for (var i = 0; i < gameplay.Tower.GuardianSpace.Blocks; i++)
                {
                    if (gameplay.Tower.GuardianSpace.TowerBlocks[i].Guardian.Equals(m_boughtGuardian))
                    {
                        found = true;
                        gameplay.Tower.GuardianSpace.TowerBlocks[i].Guardian = null;
                        break;
                    }
                }
            }
            if (found)
                gameplay.Inventory.Coins.Set(gameplay.Inventory.Coins.Get() + CoinDifference);
            return found;
        }
    }
}
