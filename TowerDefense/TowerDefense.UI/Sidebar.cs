using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using TowerDefense.GameEngine;
using TowerDefense.UI.Stylers;

namespace TowerDefense.UI
{
    public sealed class Sidebar : ClickableContainer
    {
        private bool m_hasChanged;
        private Image m_image;
        public override Image Image
        {
            get
            {
                if (m_hasChanged || Clickables.OfType<IButton>().Any(button => button.HasChanged))
                {
                    Draw();
                }
                return m_image;
            }
            set => m_image = value;
        }

        public Sidebar(Styler styler, Vector2 size) : base(styler)
        {
            Size = size;
            m_image = new Bitmap((int)Size.X, (int)Size.Y);
            Position = Config.SideBarPosition;
            ClearButtons();
            Draw();
        }

        public void ClearButtons()
        {
            Clickables = new List<IClickable>(6);
            Draw();
            // TODO move out
            /*
            Clickables.Add(new Button(new ClickableStyler(), _ => Debug.WriteLine("guardian view"), new Vector2(230, 175))
            {
                Description = "Selected Guardian View",
                Position = new Vector2(Config.SideBarButtonMargins.X, 70)
            });*/
        }

        public void AddButton(string description, Action onClickAction)
        {
            Clickables.Add(new Button(new ClickableStyler(), _ => onClickAction(), Config.SideBarButtonSize)
            {
                Position = new Vector2(Config.SideBarButtonMargins.X, 145 + Config.SideBarButtonMargins.Y * (Clickables.Count + 1) + Config.SideBarButtonSize.Y * Clickables.Count),
                Description = description
            });
            m_hasChanged = true;
        }

        public void AddButton(IButton button)
        {
            button.Position = new Vector2(Config.SideBarButtonMargins.X, 145 + Config.SideBarButtonMargins.Y * (Clickables.Count + 1) + Config.SideBarButtonSize.Y * Clickables.Count);
            button.Size = Config.SideBarButtonSize;
            Clickables.Add(button);
            m_hasChanged = true;
        }

        protected override void Draw()
        {
            m_hasChanged = false;
            var g = Graphics.FromImage(m_image);
            Styler.DrawRectangle(g, Vector2.Zero, Size);
            foreach (var clickable in Clickables)
            {
                g.DrawImage(clickable.Image, clickable.Position.X, clickable.Position.Y);
            }
            g.Dispose();
        }
    }
}
