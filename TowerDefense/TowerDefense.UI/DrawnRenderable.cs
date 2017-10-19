using System.Drawing;
using System.Numerics;
using TowerDefense.UI.Stylers;

namespace TowerDefense.UI
{
    public abstract class DrawnRenderable : IRenderable
    {
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Size { get; set; }
        public virtual Image Image { get; set; }

        protected Styler Styler { get; set; }

        protected DrawnRenderable(Styler styler)
        {
            Styler = styler;
        }

        protected abstract void Draw();
    }
}
