using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefense.GameEngine
{
    public interface IGameInfo
    {
        int? TowerHealthPoints { get; }
        int? TowerMaxHealthPoints { get; }
        int? TowerManaPoints { get; }
        int? TowerMaxManaPoints { get; }
        int? Coins { get; }
        void Subscribe(IGameInfoSubscriber subscriber);
        void Unsubscribe(IGameInfoSubscriber subscriber);
    }
}
