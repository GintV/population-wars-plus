using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using TowerDefense.UI.Stylers;

namespace TowerDefense.UI
{
    public sealed class Sidebar : ClickableContainer
    {
        public Sidebar(Styler styler, Vector2 size) : base(styler)
        {
            Clickables = new List<IClickable>(6);
            Size = size;
            Image = new Bitmap((int)Size.X, (int)Size.Y);
            Position = Config.SideBarPosition;
            Clickables.Add(new Button(new ClickableStyler(), _ => Debug.WriteLine("guardian view"), new Vector2(230, 175))
            {
                Description = "Selected Guardian View",
                Position = new Vector2(Config.SideBarButtonMargins.X, 70)
            });
            Draw();
        }

        public void AddButton(string description, Action onClickAction)
        {
            Clickables.Add(new Button(new ClickableStyler(), _ => onClickAction(), Config.SideBarButtonSize)
            {
                Position = new Vector2(Config.SideBarButtonMargins.X, 145 + Config.SideBarButtonMargins.Y * (Clickables.Count + 1) + Config.SideBarButtonSize.Y * Clickables.Count),
                Description = description
            });
            Draw();
        }

        public void ReplaceButtons(IEnumerable<IClickable> buttons)
        {
            Clickables.Clear();
            foreach (var button in buttons)
            {
                Clickables.Add(button);
            }
            Draw();
        }

        protected override void Draw()
        {
            var g = Graphics.FromImage(Image);
            Styler.DrawRectangle(g, Vector2.Zero, Size);
            foreach (var clickable in Clickables)
            {
                g.DrawImage(clickable.Image, clickable.Position.X, clickable.Position.Y);
            }
            g.Dispose();
        }
    }
}
