using System;
using System.Drawing;
using System.Numerics;

namespace TowerDefense.UI
{
    public class BasicSelectable : ISelectable
    {
        private readonly Action m_selectAction;
        private readonly Action m_deselectAction;
        public BasicSelectable(Action selectAction, Action deselectAction)
        {
            m_selectAction = selectAction;
            m_deselectAction = deselectAction;
        }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Image Image { get; set; }

        public void OnClick(Vector2 clickPosition) => m_selectAction();

        public void OnDeselect() => m_deselectAction?.Invoke();
    }
}
