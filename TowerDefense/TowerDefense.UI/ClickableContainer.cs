using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using TowerDefense.UI.Stylers;

namespace TowerDefense.UI
{
    public abstract class ClickableContainer : DrawnRenderable, IClickable
    {
        protected ICollection<IClickable> Clickables;

        public virtual void OnClick(Vector2 clickPosition)
        {
            var clicked = Clickables.FirstOrDefault(c => IsClicked(c, clickPosition));
            clicked?.OnClick(new Vector2(clickPosition.X - clicked.Position.X, clickPosition.Y - clicked.Position.Y));
        }

        protected bool IsClicked(IRenderable clickable, Vector2 click)
        {
            return clickable.Position.X <= click.X && clickable.Position.Y <= click.Y &&
                   clickable.Position.X + clickable.Size.X >= click.X && clickable.Position.Y + clickable.Size.Y >= click.Y;
        }

        protected ClickableContainer(Styler styler) : base(styler)
        {
        }
    }
}
