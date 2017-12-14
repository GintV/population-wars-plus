using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.UI.Stylers;

namespace TowerDefense.UI
{
    public sealed class DropUp : ClickableContainer, IButton
    {
        private readonly Image m_buttonImage;
        private Vector2 m_togglePosition;
        private Image m_image;
        private readonly Styler m_backStyler = new InactiveStyler();

        public bool HasChanged { get; private set; }
        public bool IsOpen { get; private set; }

        public override Image Image
        {
            get
            {
                if (HasChanged || Clickables.OfType<IButton>().Any(button => button.HasChanged))
                {
                    Draw();
                }
                return m_image;
            }
            set => m_image = value;
        }

        public DropUp(Styler styler, string label, ICollection<IClickable> clickables, bool isOpen = false) : base(styler)
        {
            IsOpen = isOpen;
            Clickables = clickables;
            Size = new Vector2(Config.SideBarButtonSize.X, Config.SideBarButtonMargins.Y / 2 * clickables.Count +
                   Config.SideBarButtonSize.Y * (clickables.Count + 1));
            Image = new Bitmap((int)Size.X, (int)Size.Y);
            m_togglePosition = new Vector2(0, (Config.SideBarButtonMargins.Y / 2 + Config.SideBarButtonSize.Y) * clickables.Count);

            var toggleSize = new Vector2(Config.SideBarButtonSize.X, Config.SideBarButtonSize.Y);
            m_buttonImage = new Bitmap((int)toggleSize.X, (int)toggleSize.Y);
            var g = Graphics.FromImage(m_buttonImage);
            Styler.DrawRectangle(g, Vector2.Zero, toggleSize);
            Styler.DrawString(g, label, Vector2.Zero, toggleSize, Config.CenterAlignFormat);
            g.Dispose();
            Draw();
        }

        public override void OnClick(Vector2 clickPosition)
        {
            if (m_togglePosition.X <= clickPosition.X && m_togglePosition.Y <= clickPosition.Y &&
                m_togglePosition.X + Size.X >= clickPosition.X &&
                m_togglePosition.Y + Config.SideBarButtonSize.Y >= clickPosition.Y)
            {
                IsOpen = !IsOpen;
                HasChanged = true;
                return;
            }
            if (IsOpen)
                base.OnClick(clickPosition);
        }

        protected override void Draw()
        {
            HasChanged = false;
            var g = Graphics.FromImage(m_image);
            g.Clear(Color.Transparent);
            if (IsOpen)
            {
                m_backStyler.DrawRectangle(g, Vector2.Zero, Size);
                foreach (var clickable in Clickables)
                {
                    g.DrawImage(clickable.Image, clickable.Position.X, clickable.Position.Y);
                }
            }
            g.DrawImage(m_buttonImage, m_togglePosition.X, m_togglePosition.Y);
            g.Dispose();
        }
    }
}
