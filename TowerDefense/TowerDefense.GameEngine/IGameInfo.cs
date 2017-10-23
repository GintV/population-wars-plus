using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source.Utils;

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

    internal class GameInfo : IGameInfo
    {
        private readonly Observer<int> m_coinsObserver;
        private readonly Observer<int> m_healthObserver;
        private readonly Observer<int> m_maxHealthObserver;
        private readonly Observer<int> m_manaObserver;
        private readonly Observer<int> m_maxManaObserver;
        private readonly List<IGameInfoSubscriber> m_subscribers;

        public int? TowerHealthPoints => m_healthObserver?.Get();
        public int? TowerMaxHealthPoints => m_maxHealthObserver?.Get();
        public int? TowerManaPoints => m_manaObserver?.Get();
        public int? TowerMaxManaPoints => m_maxManaObserver?.Get();
        public int? Coins => m_coinsObserver?.Get();

        public GameInfo(Observable<int> coinsObservable, Observable<int> healthObservable,
            Observable<int> maxHealthObservable, Observable<int> manaObservable, Observable<int> maxManaObservable)
        {
            m_coinsObserver = new Observer<int>(coinsObservable, OnCoinsChanged);
            m_coinsObserver.Get();
            m_healthObserver = new Observer<int>(healthObservable, OnHealthChanged);
            m_healthObserver.Get();
            m_maxHealthObserver = new Observer<int>(maxHealthObservable, OnHealthChanged);
            m_maxHealthObserver.Get();
            m_manaObserver = new Observer<int>(manaObservable, OnManaChanged);
            m_manaObserver.Get();
            m_maxManaObserver = new Observer<int>(maxManaObservable, OnManaChanged);
            m_maxManaObserver.Get();
            m_subscribers = new List<IGameInfoSubscriber>();
        }

        public void Subscribe(IGameInfoSubscriber subscriber)
        {
            m_subscribers.Add(subscriber);
        }

        public void Unsubscribe(IGameInfoSubscriber subscriber)
        {
            m_subscribers.Remove(subscriber);
        }

        private void OnCoinsChanged()
        {
            foreach (var subscriber in m_subscribers)
            {
                subscriber.OnCoinsChanged();
            }
        }

        private void OnHealthChanged()
        {
            foreach (var subscriber in m_subscribers)
            {
                subscriber.OnTowerHealthPointsChanged();
            }
        }

        private void OnManaChanged()
        {
            foreach (var subscriber in m_subscribers)
            {
                subscriber.OnTowerManaPointsChanged();
            }
        }
    }
}
