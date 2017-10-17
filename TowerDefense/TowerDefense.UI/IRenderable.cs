using System.Drawing;
using System.Numerics;

namespace TowerDefense.UI
{
    public interface IRenderable
    {
        Vector2 Position { get; }
        Vector2 Size { get; }
        Image Image { get; }
    }
}
