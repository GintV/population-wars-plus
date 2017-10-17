using System.Numerics;

namespace TowerDefense.UI
{
    public interface IClickable : IRenderable
    {
        void OnClick(Vector2 clickPosition);
    }
}
