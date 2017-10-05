using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.UI
{
    public interface IRenderable
    {
        Vector2 Position { get; set; }
        Vector2 Size { get; set; }
        Image Image { get; set; }
    }
}
