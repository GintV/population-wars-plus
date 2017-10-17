using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;

namespace TowerDefense.UI
{
    public class Sidebar : IClickable
    {
        private readonly ICollection<IClickable> m_clickables;

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Image Image { get; }

        public Sidebar(Vector2 size)
        {
            m_clickables = new List<IClickable>(6);
            Size = size;
            Image = new Bitmap((int)Size.X, (int)Size.Y);
            m_clickables.Add(new Button(_ => Debug.WriteLine("guardian view"), new Vector2(230, 175)) {Description = "Selected Guardian View", Position = new Vector2(Config.SideBarButtonMargins.X, 70)});
            DrawSidebar();
        }

        public void AddButton(string description, Action onClickAction)
        {
            m_clickables.Add(new Button(_ => onClickAction(), Config.SideBarButtonSize)
            {
                Position = new Vector2(Config.SideBarButtonMargins.X, 145 + Config.SideBarButtonMargins.Y * (m_clickables.Count + 1) + Config.SideBarButtonSize.Y * m_clickables.Count),
                Description = description
            });
            DrawSidebar();
        }

        public void RemoveButton(IClickable button)
        {
            m_clickables.Remove(button);
            DrawSidebar();
        }

        public void ReplaceButtons(IEnumerable<IClickable> buttons)
        {
            m_clickables.Clear();
            foreach (var button in buttons)
            {
                m_clickables.Add(button);
            }
            DrawSidebar();
        }

        public void OnClick(Vector2 clickPosition)
        {
            foreach (var clickable in m_clickables)
            {
                if (!IsClicked(clickable, clickPosition)) continue;
                clickable.OnClick(new Vector2(clickPosition.X - clickable.Position.X, clickPosition.Y - clickable.Position.Y));
                return;
            }
            Debug.Write("X: " + clickPosition.X + " Y: " + clickPosition.Y + "\n");
        }

        private static bool IsClicked(IRenderable clickable, Vector2 click)
        {
            return clickable.Position.X <= click.X && clickable.Position.Y <= click.Y &&
                   clickable.Position.X + clickable.Size.X >= click.X && clickable.Position.Y + clickable.Size.Y >= click.Y;
        }

        private void DrawSidebar()
        {
            var g = Graphics.FromImage(Image);
            g.Clear(Config.UiBackColor);
            g.DrawRectangle(Config.OutlinePen, new Rectangle(0, 0, (int)Size.X, (int)Size.Y));
            foreach (var clickable in m_clickables)
            {
                g.DrawImage(clickable.Image, clickable.Position.X, clickable.Position.Y);
            }
            g.Dispose();
        }
    }
}
