using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.UI
{
    public class StartedGameView : IView
    {
        private readonly GameView m_renderingView;

        public StartedGameView(GameView renderingView)
        {
            m_renderingView = renderingView;
        }

        public void Render(IEnumerable<IRenderable> renderables)
        {
            m_renderingView.Render(renderables);
        }
    }
}
