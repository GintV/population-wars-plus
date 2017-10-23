using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.UI
{
    public class Guardian : IRenderable
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Image Image { get; set; }
    }
}
