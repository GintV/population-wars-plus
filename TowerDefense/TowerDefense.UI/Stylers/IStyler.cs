using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Numerics;

namespace TowerDefense.UI.Stylers
{
    public abstract class Styler
    {
        public abstract void DrawRectangle(Graphics g, Vector2 position, Vector2 size);
        public abstract void DrawString(Graphics g, string str, Vector2 position, Vector2 size, StringFormat format);
        public abstract void DrawPath(Graphics g, GraphicsPath path);

        protected void ConfigureGraphics(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        }
    }
}
