using System;
using System.Collections.Generic;
using System.Numerics;
using TowerDefense.GameEngine;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Monsters;

namespace TowerDefense.UI.MockEngine
{
    public class Game2 : IGame, IGameInfo
    {
        private readonly List<Monster> m_monsters = new List<Monster>();
        private readonly List<Source.Attacks.Projectiles.Projectile> m_projectiles = new List<Source.Attacks.Projectiles.Projectile>();
        private readonly Source.Tower m_tower = new Source.Tower();
        private readonly Random m_random = new Random();
        private readonly List<IGameInfoSubscriber> m_subscribers = new List<IGameInfoSubscriber>();
        private readonly RendererToViewAdapter m_adapter;

        public Game2()
        {
            m_adapter = new RendererToViewAdapter(ViewFactory.CreateView(ViewType.NewGameView, this));
        }

        public void ConstructEnemy()
        {
            Monster createdMonster;
            var randomInt = m_random.Next(2);
            if (randomInt == 1)
            {
                createdMonster = new Skull
                {
                    Location = new Vector2(m_random.Next(1300), m_random.Next(700))
                };
            }
            else
            {
                createdMonster = new Bubble
                {
                    Location = new Vector2(m_random.Next(1300), m_random.Next(700))
                };
            }
            m_monsters.Add(createdMonster);
        }

        public void ConstructProjectile()
        {
            m_projectiles.Add(new Arrow (0,0) { Location = new Vector2(m_random.Next(1300), m_random.Next(700)) });
        }

        public void DoStuff()
        {
            throw new NotImplementedException();
        }

        public void UpgradeTower()
        {
            m_tower.Upgrade();
            foreach (var subscriber in m_subscribers)
            {
                subscriber.OnTowerHealthPointsChanged();
                subscriber.OnTowerManaPointsChanged();
            }
        }

        public void GameLoop()
        {
            while (true)
            {
                m_adapter.Render(m_tower, m_monsters, m_projectiles);
                System.Threading.Thread.Sleep(25);
            }
        }

        public int? TowerHealthPoints => m_tower.HealthPointsRemaining.Get();
        public int? TowerMaxHealthPoints => m_tower.HealthPoints.Get();
        public int? TowerManaPoints => m_tower.ManaPointsRemaining.Get();
        public int? TowerMaxManaPoints => m_tower.ManaPoints.Get();
        public int? Coins => m_random.Next(9999);
        public void Subscribe(IGameInfoSubscriber subscriber)
        {
            m_subscribers.Add(subscriber);
        }

        public void Unsubscribe(IGameInfoSubscriber subscriber)
        {
            m_subscribers.Remove(subscriber);
        }
    }
}
