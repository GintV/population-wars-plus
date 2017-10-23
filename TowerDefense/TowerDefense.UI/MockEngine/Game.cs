using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TowerDefense.GameEngine;
using TowerDefense.UI.Properties;

namespace TowerDefense.UI.MockEngine
{
    public class Game : GameInfo, IGame
    {
        private DateTime m_lastFrameTime;
        private readonly ICollection<IRenderable> m_renderableObjects;
        private readonly Stack<IRenderable> m_objectsToDestroy;
        private IView m_gameView;
        private readonly Tower m_tower;
        private readonly ICollection<IGameInfoSubscriber> m_subscribers;
        private int m_coins;

        public Vector2 Boundries { get; }
        public override int? TowerHealthPoints => m_tower.Health;
        public override int? TowerMaxHealthPoints => m_tower.MaxHealth;
        public override int? TowerManaPoints => m_tower.Mana;
        public override int? TowerMaxManaPoints => m_tower.MaxMana;
        public override int? Coins => m_coins;

        public Game()
        {
            Boundries = new Vector2 { X = 1600, Y = 900 };
            m_tower = new Tower(new Vector2(300, 780), new Vector2(125, 125), 1)
            {
                Health = 174,
                MaxHealth = 200,
                Mana = 23,
                MaxMana = 50
            };
            m_subscribers = new List<IGameInfoSubscriber>();
            m_renderableObjects = new List<IRenderable> { m_tower };
            m_gameView = ViewFactory.CreateView(ViewType.NewGameView, this);
            m_objectsToDestroy = new Stack<IRenderable>();
            m_coins = 0;
        }

        public void GameLoop()
        {
            while (true)
            {
                var currentTime = DateTime.Now;
                var deltaTime = (currentTime - m_lastFrameTime).Ticks / 10000;
                var renderables = m_renderableObjects.ToList();
                foreach (var movable in renderables.OfType<IMovable>())
                {
                    movable.Move(deltaTime);
                }
                while (m_objectsToDestroy.Count != 0)
                {
                    m_renderableObjects.Remove(m_objectsToDestroy.Pop());
                }
                m_gameView.Render(renderables);
                m_lastFrameTime = currentTime;
                System.Threading.Thread.Sleep(7);
            }
        }

        public void StartGame()
        {
            m_gameView = ViewFactory.CreateView(ViewType.StartedGameView, this);
        }

        public void UpgradeTower()
        {
            m_tower.Upgrade();
            foreach (var sub in m_subscribers)
            {
                sub.OnTowerHealthPointsChanged();
                sub.OnTowerManaPointsChanged();
            }
        }

        public void Destroy(IRenderable obj)
        {
            if (obj is Enemy)
            {
                m_coins += 10;
                foreach (var sub in m_subscribers)
                {
                    sub.OnCoinsChanged();
                }
            }
            m_objectsToDestroy.Push(obj);
        }

        public void ConstructProjectile()
        {
            m_renderableObjects.Add(new Projectile(0.8f, m_renderableObjects.OfType<Enemy>().FirstOrDefault(), this)
            {
                Position = new Vector2 { X = 525, Y = 350 },
                Size = new Vector2 { X = 15, Y = 15 },
                Image = Resources.proj
            });
        }

        public void ConstructEnemy()
        {
            m_renderableObjects.Add(new Enemy(0.2f, this, new Vector2 { X = 415, Y = 400 })
            {
                Position = new Vector2 { X = 1600, Y = 443 },
                Size = new Vector2 { X = 50, Y = 50 },
                Image = Resources.enem
            });
        }

        public void DoStuff()
        {
            m_tower.Health++;
            m_tower.Mana++;
            foreach (var sub in m_subscribers)
            {
                sub.OnTowerHealthPointsChanged();
                sub.OnTowerManaPointsChanged();
            }
        }

        public void AddMoneys()
        {
            m_coins += 170;
            foreach (var sub in m_subscribers)
            {
                sub.OnCoinsChanged();
            }
        }
    }
}
