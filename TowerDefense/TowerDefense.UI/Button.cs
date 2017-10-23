using System;
using System.Drawing;
using System.Numerics;
using TowerDefense.UI.Stylers;

namespace TowerDefense.UI
{
    public sealed class Button : DrawnRenderable, IButton
    {
        private string m_description;

        public Button(Styler styler, Action<Vector2> onClick, Vector2 size) : base(styler)
        {
            OnClickAction = onClick;
            m_description = "";
            Size = size;
            Image = new Bitmap((int)Size.X, (int)Size.Y);
            Draw();
        }

        public Action<Vector2> OnClickAction { get; set; }

        public string Description
        {
            get => m_description;
            set
            {
                m_description = value;
                Draw();
            }
        }

        public void OnClick(Vector2 clickPosition) => OnClickAction?.Invoke(clickPosition);

        protected override void Draw()
        {
            var g = Graphics.FromImage(Image);
            Styler.DrawRectangle(g, Vector2.Zero, Size);
            Styler.DrawString(g, m_description, Vector2.Zero, Size, Config.CenterAlignFormat);
            g.Dispose();
        }

        public bool HasChanged => false;
    }
}
