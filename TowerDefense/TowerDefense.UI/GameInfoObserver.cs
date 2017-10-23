using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.GameEngine;
using TowerDefense.Source.Utils;

namespace TowerDefense.UI
{
    public class GameInfoObserver : IGameInfoSubscriber
    {
        private readonly IGameInfo m_gameInfo;

        public GameInfoObserver(IGameInfo gameInfo)
        {
            m_gameInfo = gameInfo;
            Health = new Observable<int>();
            MaxHealth = new Observable<int>();
            Mana = new Observable<int>();
            MaxMana = new Observable<int>();
            Coins = new Observable<int>();
        }

        public Observable<int> Health { get; }
        public Observable<int> MaxHealth { get; }
        public Observable<int> Mana { get; }
        public Observable<int> MaxMana { get; }
        public Observable<int> Coins { get; }

        public void OnTowerHealthPointsChanged()
        {
            var newHealth = m_gameInfo.TowerHealthPoints;
            if (newHealth != null && newHealth != Health.Get())
            {
                Health.Set(newHealth.Value);
            }
            var newMaxHealth = m_gameInfo.TowerMaxHealthPoints;
            if (newMaxHealth != null && newMaxHealth != MaxHealth.Get())
            {
                MaxHealth.Set(newMaxHealth.Value);
            }
        }

        public void OnTowerManaPointsChanged()
        {
            Mana.Set(m_gameInfo.TowerManaPoints ?? 0);
        }

        public void OnCoinsChanged()
        {
            Coins.Set(m_gameInfo.Coins ?? 0);
        }

        public void OnUpdate(IGameInfo gameInfo)
        {
            throw new NotImplementedException();
        }
    }
}
