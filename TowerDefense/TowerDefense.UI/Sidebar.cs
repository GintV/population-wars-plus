using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;

namespace TowerDefense.UI
{
    public class Sidebar : ClickableContainer
    {
        public Sidebar(Vector2 size)
        {
            Clickables = new List<IClickable>(6);
            Size = size;
            Image = new Bitmap((int)Size.X, (int)Size.Y);
            Position = Config.SideBarPosition;
            Clickables.Add(new Button(_ => Debug.WriteLine("guardian view"), new Vector2(230, 175))
            {
                Description = "Selected Guardian View",
                Position = new Vector2(Config.SideBarButtonMargins.X, 70)
            });
            DrawContainer();
        }

        public void AddButton(string description, Action onClickAction)
        {
            Clickables.Add(new Button(_ => onClickAction(), Config.SideBarButtonSize)
            {
                Position = new Vector2(Config.SideBarButtonMargins.X, 145 + Config.SideBarButtonMargins.Y * (Clickables.Count + 1) + Config.SideBarButtonSize.Y * Clickables.Count),
                Description = description
            });
            DrawContainer();
        }

        public void ReplaceButtons(IEnumerable<IClickable> buttons)
        {
            Clickables.Clear();
            foreach (var button in buttons)
            {
                Clickables.Add(button);
            }
            DrawContainer();
        }

        protected sealed override void DrawContainer()
        {
            var g = Graphics.FromImage(Image);
            g.Clear(Config.UiBackColor);
            g.DrawRectangle(Config.OutlinePen, new Rectangle(0, 0, (int)Size.X, (int)Size.Y));
            foreach (var clickable in Clickables)
            {
                g.DrawImage(clickable.Image, clickable.Position.X, clickable.Position.Y);
            }
            g.Dispose();
        }
    }
}
