using System.Numerics;

namespace TowerDefense.UI
{
    public interface IButton : IClickable
    {
        new Vector2 Position { get; set; }
        new Vector2 Size { get; set; }
        bool HasChanged { get; }
    }
}