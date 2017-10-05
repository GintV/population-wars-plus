using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.UI
{
    public class Sidebar : IClickable
    {
        private ICollection<IClickable> m_clickables;

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Image Image { get; set; }

        public void SetButtons(ICollection<IClickable> clickables)
        {
            m_clickables = clickables;
        }

        public void OnClick(Vector2 clickPosition)
        {
            Debug.Write("X: " + clickPosition.X + " Y: " + clickPosition.Y + "\n");
        }
    }
}
