using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.UI.Stylers
{
    public class GuiBackStyler : Styler
    {
        public override void DrawRectangle(Graphics g, Vector2 position, Vector2 size)
        {
            g.FillRectangle(Config.UiBackBrush, (int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            g.DrawRectangle(Config.OutlinePen, (int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public override void DrawString(Graphics g, string str, Vector2 position, Vector2 size, StringFormat format)
        {
            ConfigureGraphics(g);
            g.DrawString(str, Config.DefaultFont, Config.TextBrush, new RectangleF(position.X, position.Y, size.X, size.Y), format);
        }

        public override void DrawPath(Graphics g, GraphicsPath path)
        {
            g.FillPath(Config.UiBackBrush, path);
            g.DrawPath(Config.NarrowOutlinePen, path);
        }
    }
}
