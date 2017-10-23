using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Guardians;

namespace TowerDefense.GameEngine.Transactions
{
    internal class GuardianPromote : CoinTransaction
    {
        private IAttack m_previousAttackType;
        private int m_previousPromoteLevel;
        public Guardian PromotedGuardian { get; }

        public GuardianPromote(Guardian guardianToPromote)
        {
            PromotedGuardian = guardianToPromote;
        }
        public override bool Execute(IGameEnvironment environment)
        {
            CoinDifference = PromotedGuardian.PromoteCost;
            environment.Inventory.Coins.Set(environment.Inventory.Coins.Get() - CoinDifference);
            m_previousAttackType = PromotedGuardian.AttackType;
            m_previousPromoteLevel = PromotedGuardian.PromoteLevel;
            PromotedGuardian.Promote();
            return true;
        }

        public override bool Undo(IGameEnvironment environment)
        {
            environment.Inventory.Coins.Set(environment.Inventory.Coins.Get() + CoinDifference);
            PromotedGuardian.Demote(m_previousAttackType, m_previousPromoteLevel);
            return true;
        }
    }
}
