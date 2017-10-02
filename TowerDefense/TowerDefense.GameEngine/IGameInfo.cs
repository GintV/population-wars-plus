using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefense.GameEngine
{
    public interface IGameInfo
    {
        int? TowerHealthPoints { get; }
        int? TowerManaPoints { get; }
        int? Coins { get; }
        void Subscribe(IGameInfoSubscriber subscriber);
    }
}
