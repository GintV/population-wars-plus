using System;
using System.Drawing;
using System.Numerics;

namespace TowerDefense.UI
{
    public class BasicClickable : IClickable
    {
        private readonly Action<Vector2> m_action;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Image Image { get; set; }
        public BasicClickable(Action<Vector2> action) => m_action = action;
        public void OnClick(Vector2 clickPosition) => m_action?.Invoke(clickPosition);
    }
}
