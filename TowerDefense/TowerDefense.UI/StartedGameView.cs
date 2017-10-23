using System.Collections.Generic;

namespace TowerDefense.UI
{
    public class StartedGameView : IView
    {
        private readonly GameView m_renderingView;
        private bool m_isInstansiated;

        public StartedGameView(GameView renderingView)
        {
            m_renderingView = renderingView;
            m_isInstansiated = false;
        }

        public void Render(IEnumerable<IRenderable> renderables)
        {
            if (!m_isInstansiated)
                Instantiate();
            m_renderingView.Render(renderables);
        }

        public void RegisterClickable(IClickable clickable)
        {
            throw new System.NotImplementedException();
        }

        public void DeregisterClickable(IClickable clickable)
        {
            throw new System.NotImplementedException();
        }

        public void ClearClickables()
        {
            throw new System.NotImplementedException();
        }

        public ISelectable GetSelectedSelectable()
        {
            throw new System.NotImplementedException();
        }

        public void Instantiate()
        {
            if (m_isInstansiated) return;
            m_isInstansiated = true;
            m_renderingView.RegisteredClickables.Clear();
        }
    }
}
