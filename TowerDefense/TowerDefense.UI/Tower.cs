using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class Tower : IRenderable
    {
        public Dims Position { get; set; }
        public Dims Size { get; set; }
        public Image Image { get; set; }
    }
}
