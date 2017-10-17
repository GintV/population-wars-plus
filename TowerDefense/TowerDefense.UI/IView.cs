using System.Collections.Generic;

namespace TowerDefense.UI
{
    public interface IView
    {
        void Render(IEnumerable<IRenderable> renderables);
    }
}
