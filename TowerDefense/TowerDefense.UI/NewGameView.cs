using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.UI.Properties;

namespace TowerDefense.UI
{
    public class NewGameView : IView
    {
        private readonly GameView m_renderingView;

        public NewGameView(GameView renderingView)
        {
            m_renderingView = renderingView;
            renderingView.RegisteredClickables.Add(new Sidebar
            {
                Image = Resources.proj,
                Position = new Vector2(400, 25),
                Size = new Vector2(25, 25)
            });
            renderingView.RegisteredClickables.Add(new ConcreteSelectable
            {
                Image = Resources.proj,
                Position = new Vector2(300, 25),
                Size = new Vector2(25, 25)
            });
        }

        public void Render(IEnumerable<IRenderable> renderables)
        {
            m_renderingView.Render(renderables.Concat(new List<IRenderable> {new Projectile(0, null, null)
            {
                Image = Resources.proj,
                Position = new Vector2(500, 25),
                Size = new Vector2(25,25)
            }}));
        }
    }
}
