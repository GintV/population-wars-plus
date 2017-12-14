using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.Source.Utils;
using TowerDefense.UI.Stylers;

namespace TowerDefense.UI
{
    public sealed class DependantButton<T> : DrawnRenderable, IButton
    {
        private readonly string m_description;
        private readonly Styler m_activeStyler;
        private readonly Styler m_inactiveStlyer;
        private readonly Observer<T> m_valueObserver;
        private readonly Func<T, bool> m_isActiveCheck;
        private Image m_image;
        private Vector2 m_size;
        private bool m_isActive;
        private bool m_needsEvaluation;

        public Action<Vector2> OnClickAction { get; set; }

        public override Image Image
        {
            get
            {
                if (m_needsEvaluation)
                {
                    ResolveStyler();
                }
                if (HasChanged)
                {
                    Draw();
                }
                return m_image;
            }
        }

        public override Vector2 Size { get => m_size; set { m_size = value; m_image = new Bitmap((int)Size.X, (int)Size.Y); Draw(); } }
        public bool HasChanged { get; private set; }

        public DependantButton(string description, Vector2 size, Styler active, Styler inactive, Observable<T> observedValue, Func<T, bool> isActiveCheck) : base(active)
        {
            m_description = description;
            m_activeStyler = active;
            m_inactiveStlyer = inactive;
            m_valueObserver = new Observer<T>(observedValue, () => { m_needsEvaluation = true; });
            m_isActiveCheck = isActiveCheck;
            m_isActive = true;
            Size = size;
            ResolveStyler();
        }

        protected override void Draw()
        {
            var g = Graphics.FromImage(m_image);
            Styler.DrawRectangle(g, Vector2.Zero, Size);
            Styler.DrawString(g, m_description, Vector2.Zero, Size, Config.CenterAlignFormat);
            g.Dispose();
            HasChanged = false;
        }

        private void ResolveStyler()
        {
            var neededState = m_isActiveCheck(m_valueObserver.Get());
            if (m_isActive != neededState)
            {
                Styler = neededState ? m_activeStyler : m_inactiveStlyer;
                m_isActive = neededState;
                HasChanged = true;
            }
        }

        public void OnClick(Vector2 clickPosition)
        {
            if (m_isActive)
                OnClickAction?.Invoke(clickPosition);
        }
    }
}
