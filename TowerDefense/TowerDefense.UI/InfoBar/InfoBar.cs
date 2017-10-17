using System;
using System.Drawing;
using System.Numerics;
using TowerDefense.GameEngine;

namespace TowerDefense.UI.InfoBar
{
    public class InfoBar : IRenderable, IGameInfoSubscriber
    {
        private readonly IGameInfo m_gameInfo;
        private Image m_image;
        private readonly GameInfoBlock m_healthBlock;
        private readonly GameInfoBlock m_manaBlock;
        private readonly GameInfoBlock m_coinsBlock;
        private readonly GameInfoBlock m_timeBlock;
        private bool m_hasChanged;
        private readonly Rectangle m_boundingRectangle;

        public Vector2 Position { get; }
        public Vector2 Size { get; }
        public Image Image
        {
            get
            {
                if (m_hasChanged)
                    DrawInfoBar();
                return m_image;
            }
            private set => m_image = value;
        }

        public InfoBar(IGameInfo gameInfo)
        {
            Position = Config.InfoBarPosition;
            Size = Config.InfoBarSize;
            m_coinsBlock = new GameInfoBlock(Config.GameInfoBlockMargins, "Coins");
            m_healthBlock = new GameInfoBlock(new Vector2(Config.GameInfoBlockMargins.X * 2 + Config.GameInfoBlockSize.X, Config.GameInfoBlockMargins.Y), "Health");
            m_manaBlock = new GameInfoBlock(new Vector2(Config.GameInfoBlockMargins.X * 3 + Config.GameInfoBlockSize.X * 2, Config.GameInfoBlockMargins.Y), "Mana");
            m_timeBlock = new GameInfoBlock(new Vector2(Config.GameInfoBlockMargins.X * 4 + Config.GameInfoBlockSize.X * 3, Config.GameInfoBlockMargins.Y), "Time");
            m_gameInfo = gameInfo;
            m_gameInfo.Subscribe(this);
            m_boundingRectangle = new Rectangle(0, 0, (int)Size.X, (int)Size.Y);
            DrawInfoBar();
        }

        public void OnTowerHealthPointsChanged()
        {
            m_healthBlock.Value = m_gameInfo.TowerHealthPoints.ToString();
            m_healthBlock.MaxValue = m_gameInfo.TowerMaxHealthPoints.ToString();
            m_hasChanged = true;
        }

        public void OnTowerManaPointsChanged()
        {
            m_manaBlock.Value = m_gameInfo.TowerManaPoints.ToString();
            m_manaBlock.MaxValue = m_gameInfo.TowerMaxManaPoints.ToString();
            m_hasChanged = true;
        }

        public void OnCoinsChanged()
        {
            m_coinsBlock.Value = m_gameInfo.Coins.ToString();
            m_hasChanged = true;
        }

        public void OnUpdate(IGameInfo gameInfo)
        {
            throw new NotImplementedException();
        }

        private void DrawInfoBar()
        {
            var newImage = new Bitmap((int)Size.X, (int)Size.Y);
            var g = Graphics.FromImage(newImage);
            g.FillRectangle(Config.UiBackBrush, m_boundingRectangle);
            g.DrawRectangle(Config.OutlinePen, m_boundingRectangle);
            g.DrawImage(m_coinsBlock.Image, m_coinsBlock.Position.X, m_coinsBlock.Position.Y);
            g.DrawImage(m_healthBlock.Image, m_healthBlock.Position.X, m_healthBlock.Position.Y);
            g.DrawImage(m_manaBlock.Image, m_manaBlock.Position.X, m_manaBlock.Position.Y);
            g.DrawImage(m_timeBlock.Image, m_timeBlock.Position.X, m_timeBlock.Position.Y);
            m_hasChanged = false;
            Image = newImage;
        }
    }
}
