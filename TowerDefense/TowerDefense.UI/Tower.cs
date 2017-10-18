using System.Drawing;
using System.Numerics;
using TowerDefense.UI.Properties;

namespace TowerDefense.UI
{
    public class Tower : IRenderable
    {
        private Image m_image;
        private bool m_hasChanged;
        private int m_level;
        private Vector2 m_baseSize;
        private Vector2 m_basePosition;


        public Vector2 Position { get; private set; }
        public Vector2 Size { get; private set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int MaxHealth { get; set; }
        public int MaxMana { get; set; }
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

        public int Level
        {
            get => m_level;
            set
            {
                m_level = value;
                Size = new Vector2(m_baseSize.X, m_baseSize.Y * (value + 1));
                Position = new Vector2(m_basePosition.X, m_basePosition.Y - Size.Y);
                m_hasChanged = true;
            }
        }

        public Tower(Vector2 basePosition, Vector2 baseSize, int level = 0)
        {
            m_basePosition = basePosition;
            m_baseSize = baseSize;
            Level = level;
        }

        public void Upgrade()
        {
            MaxHealth += 10;
            MaxMana += 10;
            Level++;
            m_hasChanged = true;
        }

        private void DrawTower()
        {
            m_image = new Bitmap((int)m_baseSize.X, (int)m_baseSize.Y * (Level + 1));
            var g = Graphics.FromImage(m_image);
            g.DrawImage(Resources.top, 0, 0, m_baseSize.X, m_baseSize.Y);
            for (var i = 1; i <= Level; i++)
            {
                g.DrawImage(Resources._base, 0, m_baseSize.Y * i, m_baseSize.X, m_baseSize.Y);
            }
            g.Dispose();
            m_hasChanged = false;
        }
    }
}
