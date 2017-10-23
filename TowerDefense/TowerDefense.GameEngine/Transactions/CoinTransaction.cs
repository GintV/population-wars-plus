using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefense.GameEngine.Transactions
{
    public abstract class CoinTransaction
    {
        public int CoinDifference { get; set; }

        public abstract bool Execute(IGameEnvironment environment);
        public abstract bool Undo(IGameEnvironment environment);
        public virtual bool IsUndoable() => true;
    }
}
