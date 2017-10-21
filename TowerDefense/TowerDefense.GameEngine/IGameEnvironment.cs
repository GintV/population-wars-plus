using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source;

namespace TowerDefense.GameEngine
{
    public interface IGameEnvironment
    {
        Inventory Inventory { get; }
        Tower Tower { get; }
    }

    internal class GameEnvironment : IGameEnvironment
    {
        public Inventory Inventory { get; }
        public Tower Tower { get; }
    }
}
