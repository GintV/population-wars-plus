using System;
using System.Drawing;
using System.Numerics;
using TowerDefense.GameEngine;
using TowerDefense.UI.Stylers;

namespace TowerDefense.UI.InfoBar
{
    public sealed class InfoBar : DrawnRenderable, IGameInfoSubscriber
    {
        private readonly GameInfo m_gameInfo;
        private Image m_image;
        private readonly GameInfoBlock m_healthBlock;
        private readonly GameInfoBlock m_manaBlock;
        private readonly GameInfoBlock m_coinsBlock;
        private readonly GameInfoBlock m_timeBlock;
        private bool m_hasChanged;
        private readonly Rectangle m_boundingRectangle;

        public override Image Image
        {
            get
            {
                if (m_hasChanged)
                    Draw();
                return m_image;
            }
            set => m_image = value;
        }

        public InfoBar(Styler styler, GameInfo gameInfo) : base(styler)
        {
            Position = Config.InfoBarPosition;
            Size = Config.InfoBarSize;
            m_coinsBlock = new GameInfoBlock(new ClickableStyler(), Config.GameInfoBlockMargins, "Coins");
            m_healthBlock = new GameInfoBlock(new ClickableStyler(), new Vector2(Config.GameInfoBlockMargins.X * 2 + Config.GameInfoBlockSize.X, Config.GameInfoBlockMargins.Y), "Health");
            m_manaBlock = new GameInfoBlock(new ClickableStyler(), new Vector2(Config.GameInfoBlockMargins.X * 3 + Config.GameInfoBlockSize.X * 2, Config.GameInfoBlockMargins.Y), "Mana");
            m_timeBlock = new GameInfoBlock(new ClickableStyler(), new Vector2(Config.GameInfoBlockMargins.X * 4 + Config.GameInfoBlockSize.X * 3, Config.GameInfoBlockMargins.Y), "Time");
            m_gameInfo = gameInfo;
            m_gameInfo.Subscribe(this);
            m_boundingRectangle = new Rectangle(0, 0, (int)Size.X, (int)Size.Y);
            Draw();
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

        protected override void Draw()
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
