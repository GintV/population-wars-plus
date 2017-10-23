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

        public GuardianPromote(Guardian guardian)
        {
            PromotedGuardian = guardian;
        }
        public override bool Execute(IGameEnvironment gameplay)
        {
            CoinDifference = PromotedGuardian.PromoteCost;
            gameplay.Inventory.Coins.Set(gameplay.Inventory.Coins.Get() - CoinDifference);
            m_previousAttackType = PromotedGuardian.AttackType;
            m_previousPromoteLevel = PromotedGuardian.PromoteLevel;
            PromotedGuardian.Promote();
            return true;
        }

        public override bool Undo(IGameEnvironment gameplay)
        {
            gameplay.Inventory.Coins.Set(gameplay.Inventory.Coins.Get() + CoinDifference);
            PromotedGuardian.Demote(m_previousAttackType, m_previousPromoteLevel);
            return true;
        }
    }
}
