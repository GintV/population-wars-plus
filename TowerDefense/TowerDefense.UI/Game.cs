using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.UI.Properties;

namespace TowerDefense.UI
{
    public class Game
    {
        private long m_lastFrameTime;
        private readonly ICollection<IRenderable> m_renderableObjects;
        private readonly Stack<IRenderable> m_objectsToDestroy;
        private IView m_gameView;

        public Vector2 Boundries { get; }

        public Game()
        {
            Boundries = new Vector2 { X = 820, Y = 500 };
            m_renderableObjects = new List<IRenderable>
            {
                new Tower
                {
                    Position = new Vector2 { X = 10, Y = 100 },
                    Size = new Vector2 { X = 100, Y = 400 },
                    Image = Resources.tower
                }
            };
            m_gameView = ViewFactory.CreateView(ViewType.NewGameView);
            m_objectsToDestroy = new Stack<IRenderable>();
        }

        public void GameLoop()
        {
            while (true)
            {
                var currentTime = DateTime.Now.Ticks;
                var deltaTime = currentTime - m_lastFrameTime;

                foreach (var movable in m_renderableObjects.OfType<IMovable>())
                {
                    movable.Move(deltaTime);
                }
                while (m_objectsToDestroy.Count != 0)
                {
                    m_renderableObjects.Remove(m_objectsToDestroy.Pop());
                }
                m_gameView.Render(m_renderableObjects);
                m_lastFrameTime = currentTime;
            }
        }

        public void Destroy(IRenderable obj)
        {
            m_objectsToDestroy.Push(obj);
        }

        public void ConstructProjectile()
        {
            m_renderableObjects.Add(new Projectile(8, m_renderableObjects.OfType<Enemy>().FirstOrDefault(), this)
            {
                Position = new Vector2 { X = 50, Y = 130 },
                Size = new Vector2 { X = 15, Y = 15 },
                Image = Resources.proj
            });
        }

        public void ConstructEnemy()
        {
            m_renderableObjects.Add(new Enemy(2, this, new Vector2 { X = 80, Y = 400 })
            {
                Position = new Vector2 { X = 800, Y = 443 },
                Size = new Vector2 { X = 50, Y = 50 },
                Image = Resources.enem
            });
        }
    }
}
