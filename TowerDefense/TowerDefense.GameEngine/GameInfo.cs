using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source.Utils;

namespace TowerDefense.GameEngine
{
    public abstract class GameInfo
    {
        protected readonly List<IGameInfoSubscriber> m_subscribers;

        public virtual int? TowerHealthPoints { get; }
        public virtual int? TowerMaxHealthPoints { get; }
        public virtual int? TowerManaPoints { get; }
        public virtual int? TowerMaxManaPoints { get; }
        public virtual int? Coins { get; }

        protected GameInfo()
        {
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

        protected virtual void OnCoinsChanged()
        {
            foreach (var subscriber in m_subscribers)
            {
                subscriber.OnCoinsChanged();
            }
        }

        protected virtual void OnHealthChanged()
        {
            foreach (var subscriber in m_subscribers)
            {
                subscriber.OnTowerHealthPointsChanged();
            }
        }

        protected virtual void OnManaChanged()
        {
            foreach (var subscriber in m_subscribers)
            {
                subscriber.OnTowerManaPointsChanged();
            }
        }
    }

    internal class GameInfoProvider : GameInfo
    {
        private readonly Observer<int> m_coinsObserver;
        private readonly Observer<int> m_healthObserver;
        private readonly Observer<int> m_maxHealthObserver;
        private readonly Observer<int> m_manaObserver;
        private readonly Observer<int> m_maxManaObserver;

        public override int? TowerHealthPoints => m_healthObserver?.Get();
        public override int? TowerMaxHealthPoints => m_maxHealthObserver?.Get();
        public override int? TowerManaPoints => m_manaObserver?.Get();
        public override int? TowerMaxManaPoints => m_maxManaObserver?.Get();
        public override int? Coins => m_coinsObserver?.Get();

        public GameInfoProvider(Observable<int> coinsObservable, Observable<int> healthObservable,
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
        }
    }
}
