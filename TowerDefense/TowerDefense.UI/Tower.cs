using System.Drawing;
using System.Numerics;
using TowerDefense.UI.Properties;

namespace TowerDefense.UI
{
    public class Tower : IRenderable
    {
        private Image m_image;
        private bool m_hasChanged;

        public Image Image
        {
            get
            {
                if (m_hasChanged)
                    DrawTower();
                return m_image;
            }
            set => m_image = value;
        }

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public int Health { get; set; }
        public int Mana { get; set; }
        public int MaxHealth { get; set; }
        public int MaxMana { get; set; }

        public Tower()
        {
            var top = Resources.top;
            var bottom = Resources._base;
            Image = new Bitmap(500, 1000);
            var g = Graphics.FromImage(Image);
            g.DrawImage(top, 0, 0, 500, 500);
            g.DrawImage(bottom, 0, 500, 500, 500);
            g.Dispose();
        }


        public void Upgrade()
        {
            MaxHealth += 10;
            MaxMana += 10;
            Size = new Vector2(Size.X, Size.Y + 125);
            Position = new Vector2(Position.X, Position.Y - 125);
            m_hasChanged = true;
        }

        private void DrawTower()
        {
            var image = new Bitmap(m_image.Width, m_image.Height + 500);
            var g = Graphics.FromImage(image);
            g.DrawImage(m_image, 0, 0);
            g.DrawImage(Resources._base, 0, m_image.Height, 500, 500);
            g.Dispose();
            m_image = image;
            m_hasChanged = false;
        }
    }
}
