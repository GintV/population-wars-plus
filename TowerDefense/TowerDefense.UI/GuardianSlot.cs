using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.UI
{
    public class GuardianSlot : ISelectable
    {
        private readonly Action m_onSelect;
        private readonly Action m_onDeselect;
        public GuardianSlot(Action onSelect, Action onDeselect)
        {
            m_onSelect = onSelect;
            m_onDeselect = onDeselect;
        }

        public int Index { get; set; }
        public Guardian Guardian { get; set; }
        public bool IsEmpty => Guardian == null;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Image Image => Guardian?.Image;
        public void OnClick(Vector2 clickPosition)
        {
            m_onSelect?.Invoke();
        }

        public void OnDeselect()
        {
            m_onDeselect?.Invoke();
        }
    }
}
