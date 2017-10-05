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
    public class ConcreteSelectable : ISelectable
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Image Image { get; set; }
        public void OnClick(Vector2 clickPosition)
        {
            Debug.Write("I've been selected! :D\n");
        }

        public void OnDeselect()
        {
            Debug.Write("I've been deselected! :(\n");
        }
    }
}
