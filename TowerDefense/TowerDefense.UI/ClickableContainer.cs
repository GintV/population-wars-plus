using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace TowerDefense.UI
{
    public abstract class ClickableContainer: IClickable
    {
        protected ICollection<IClickable> Clickables;
        public Vector2 Position { get; protected set; }
        public Vector2 Size { get; protected set; }
        public virtual Image Image { get; protected set; }

        public void OnClick(Vector2 clickPosition) => Clickables.FirstOrDefault(po => IsClicked(po, clickPosition))?.OnClick(clickPosition);

        protected bool IsClicked(IRenderable clickable, Vector2 click)
        {
            return clickable.Position.X <= click.X && clickable.Position.Y <= click.Y &&
                   clickable.Position.X + clickable.Size.X >= click.X && clickable.Position.Y + clickable.Size.Y >= click.Y;
        }

        protected abstract void DrawContainer();
    }
}
