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
        private Guardian m_boughtGuardian;

        public Flags.GuardianClass GuardianClass { get; }
        public Flags.GuardianType GuardianType { get; }

        public GuardianBuy(Flags.GuardianClass guardianClass, Flags.GuardianType guardianType, int cost)
        {
            GuardianClass = guardianClass;
            GuardianType = guardianType;
            CoinDifference = cost;
        }

        public override bool Execute(IGameEnvironment environment)
        {
            if (environment.Inventory.Coins.Get() < CoinDifference) return false;
            environment.Inventory.Coins.Set(environment.Inventory.Coins.Get() - CoinDifference);
            var factory = GuardianFactoryProvider.GetFactory(GuardianClass);
            if (!factory.HasValue)
                return false;
            var guardian = factory.Value.CreateGuardian(GuardianType);
            if (!guardian.HasValue)
                return false;
            m_boughtGuardian = guardian.Value;
            environment.Inventory.Guardians.Add(guardian.Value);
            environment.InventoryInfo.OnInventoryChanged();
            return true;
        }

        public override bool Undo(IGameEnvironment environment)
        {
            var found = true;
            if (!environment.Inventory.Guardians.Remove(m_boughtGuardian))
            {
                found = false;
                for (var i = 0; i < environment.Tower.GuardianSpace.Blocks; i++)
                {
                    if (environment.Tower.GuardianSpace.TowerBlocks[i].Guardian.Equals(m_boughtGuardian))
                    {
                        found = true;
                        environment.Tower.GuardianSpace.TowerBlocks[i].Guardian = null;
                        break;
                    }
                }
            }
            if (found)
                environment.Inventory.Coins.Set(environment.Inventory.Coins.Get() + CoinDifference);
            environment.InventoryInfo.OnInventoryChanged();
            return found;
        }
    }
}
