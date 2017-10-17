using System.Drawing;
using System.Numerics;

namespace TowerDefense.UI
{
    public class BasicRenderable : IRenderable
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Image Image { get; set; }
    }
}
