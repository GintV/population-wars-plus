using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Numerics;

namespace TowerDefense.UI
{
    public class Config
    {
        public static int OutlineWidth = 8;
        public static Vector2 Boundries = new Vector2(1600, 900);

        public static Vector2 InfoBarSize = new Vector2(1350, 70);
        public static Vector2 InfoBarPosition = new Vector2(250 - OutlineWidth / 2, 0);
        public static Vector2 GameInfoBlockSize = new Vector2(300, 50);
        public static Vector2 GameInfoBlockMargins = new Vector2(30, 10);

        public static Vector2 SideBarSize = new Vector2(250, 780);
        public static Vector2 SideBarPosition = new Vector2(0, 0);
        public static Vector2 SideBarButtonSize = new Vector2(230, 70);
        public static Vector2 SideBarButtonMargins = new Vector2(10, 30);

        public static Vector2 InventorySize = new Vector2(800, 120);
        public static Vector2 InventoryPosition = new Vector2(0, 780 - OutlineWidth / 2);
        public static Vector2 InventoryBlockSize = new Vector2(100, 100);
        public static Vector2 InventoryBlockMargins = new Vector2(30, 10);
        public static Vector2 InventoryArrowSize = new Vector2(52, 76);
        public static Vector2 InventoryArrowMargins = new Vector2(14, 20.5f);
        public static int InventoryItemsInViewLimit = 5;

        public static Vector2 ShopSize = new Vector2(800, 120);
        public static Vector2 ShopPosition = new Vector2(800 - OutlineWidth / 2, 780 - OutlineWidth / 2);
        public static Vector2 ShopBlockSize = new Vector2(100, 100);
        public static Vector2 ShopBlockMargins = new Vector2(30, 10);
        public static int ShopItemsLimit = 6;

        public static Color UiBackColor = Color.FromArgb(83, 71, 65);
        public static Color OutlineColor = Color.Black;
        public static Color TextColor = Color.FromArgb(150, 150, 150);
        public static Color ButtonColor = Color.FromArgb(61, 61, 61);

        public static Font DefaultFont = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Regular);
        public static Font GameInfoFont = new Font(FontFamily.GenericSansSerif, 21, FontStyle.Regular);

        public static Pen OutlinePen = new Pen(OutlineColor, OutlineWidth);
        public static Pen NarrowOutlinePen = new Pen(OutlineColor, 4);
        public static Pen SelectionPen = new Pen(Color.SteelBlue, 4);
        public static Brush TextBrush = new SolidBrush(TextColor);
        public static Brush ButtonBrush = new SolidBrush(ButtonColor);
        public static Brush UiBackBrush = new SolidBrush(UiBackColor);

        public static StringFormat CenterAlignFormat = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};


        public static void ConfigureGraphics(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        }
    }
}
