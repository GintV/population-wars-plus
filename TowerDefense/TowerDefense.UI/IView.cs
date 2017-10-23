using System.Collections.Generic;

namespace TowerDefense.UI
{
    public interface IView
    {
        void Render(IEnumerable<IRenderable> renderables);
        void RegisterClickable(IClickable clickable);
        void DeregisterClickable(IClickable clickable);
        void ClearClickables();
        ISelectable GetSelectedSelectable();
    }
}
