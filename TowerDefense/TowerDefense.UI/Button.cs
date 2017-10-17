using System;
using System.Drawing;
using System.Numerics;

namespace TowerDefense.UI
{
    public class Button : IClickable
    {
        private string m_description;
        private readonly Rectangle m_boundingRectangle;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; }
        public Image Image { get; }

        public Button(Action<Vector2> onClick, Vector2 size)
        {
            OnClickAction = onClick;
            m_description = "";
            Size = size;
            m_boundingRectangle = new Rectangle(0, 0, (int)Size.X, (int)Size.Y);
            Image = new Bitmap((int)Size.X, (int)Size.Y);
            DrawButton();
        }

        public Action<Vector2> OnClickAction { get; set; }

        public string Description
        {
            get => m_description;
            set
            {
                m_description = value;
                DrawButton();
            }
        }

        public void OnClick(Vector2 clickPosition) => OnClickAction?.Invoke(clickPosition);

        private void DrawButton()
        {
            var g = Graphics.FromImage(Image);
            Config.ConfigureGraphics(g);
            g.Clear(Config.ButtonColor);
            g.DrawRectangle(Config.OutlinePen, m_boundingRectangle);
            g.DrawString(m_description, Config.DefaultFont, Config.TextBrush, m_boundingRectangle, Config.CenterAlignFormat);
            g.Flush();
            g.Dispose();
        }
    }
}
