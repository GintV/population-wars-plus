using System.Drawing;
using System.Numerics;

namespace TowerDefense.UI.InfoBar
{
    public class GameInfoBlock : IRenderable
    {
        private string m_value;
        private string m_maxValue;
        private bool m_hasChanged;
        private readonly Rectangle m_boundingRectangle;
        private readonly Rectangle m_textRectangle;
        private Image m_image;

        public Vector2 Position { get; }
        public Vector2 Size { get; }
        public string Name { get; }

        public Image Image
        {
            get
            {
                if (m_hasChanged)
                    DrawInfoBlock();
                return m_image;
            }
            private set => m_image = value;
        }

        public string Value
        {
            get => m_value;
            set
            {
                m_value = value;
                m_hasChanged = true;
            }
        }

        public string MaxValue
        {
            get => m_maxValue;
            set
            {
                m_maxValue = value;
                m_hasChanged = true;
            }
        }


        public GameInfoBlock(Vector2 position, string name)
        {
            Size = Config.GameInfoBlockSize;
            m_boundingRectangle = new Rectangle(0, 0, (int)Size.X, (int)Size.Y);
            m_textRectangle = new Rectangle(10, 0, (int)Size.X - 20, (int)Size.Y);
            Position = position;
            Name = name;
            m_value = "0";
            m_maxValue = "";
            Image = new Bitmap((int)Size.X, (int)Size.Y);
            DrawInfoBlock();
        }


        private void DrawInfoBlock()
        {
            var g = Graphics.FromImage(m_image);
            g.Clear(Config.ButtonColor);
            Config.ConfigureGraphics(g);
            var formatNear = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
            var formatFar = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };
            g.DrawRectangle(Config.OutlinePen, m_boundingRectangle);
            g.DrawString(
                Name,
                Config.GameInfoFont,
                Config.TextBrush, m_textRectangle, formatNear);
            g.DrawString(
                $"{Value}{ (MaxValue == "" ? "" : " / " + MaxValue)}",
                Config.GameInfoFont, Config.TextBrush, m_textRectangle, formatFar);
            g.Flush();
            g.Dispose();
            m_hasChanged = false;
        }
    }
}
