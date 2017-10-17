using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace TowerDefense.UI
{
    public class Shop : IClickable
    {
        private readonly ICollection<IClickable> m_purchaseOptions;
        public Vector2 Position { get; }
        public Vector2 Size { get; }
        public Image Image { get; private set; }

        public Shop()
        {
            Position = Config.ShopPosition;
            Size = Config.ShopSize;
            m_purchaseOptions = new List<IClickable>
            {
                new Button(_ => Debug.WriteLine("Ice Wizard clicked"), Config.ShopBlockSize)
                {
                    Description = "IW",
                    Position = BlockPositionByIndex(0)
                },
                new Button(_ => Debug.WriteLine("Fire Wizard clicked"), Config.ShopBlockSize)
                {
                    Description = "FW",
                    Position = BlockPositionByIndex(1)
                },
                new Button(_ => Debug.WriteLine("Light Archer clicked"), Config.ShopBlockSize)
                {
                    Description = "LA",
                    Position = BlockPositionByIndex(2)
                },
                new Button(_ => Debug.WriteLine("Dark Archer clicked"), Config.ShopBlockSize)
                {
                    Description = "DA",
                    Position = BlockPositionByIndex(3)
                },
                new Button(_ => Debug.WriteLine("Upgrade Tower clicked"), Config.ShopBlockSize)
                {
                    Description = "UT",
                    Position = BlockPositionByIndex(4)
                },
                new Button(_ => Debug.WriteLine("Undo clicked"), Config.ShopBlockSize)
                {
                    Description = "Undo",
                    Position = BlockPositionByIndex(5)
                }
            };
            DrawImage();
        }

        public void OnClick(Vector2 clickPosition) => m_purchaseOptions.FirstOrDefault(po => IsClicked(po, clickPosition))?.OnClick(clickPosition);

        private static bool IsClicked(IRenderable clickable, Vector2 click)
        {
            return clickable.Position.X <= click.X && clickable.Position.Y <= click.Y &&
                   clickable.Position.X + clickable.Size.X >= click.X && clickable.Position.Y + clickable.Size.Y >= click.Y;
        }

        private static Vector2 BlockPositionByIndex(int i)
        {
            return new Vector2(Config.ShopBlockMargins.X * (i + 1) + Config.ShopBlockSize.X * i, Config.ShopBlockMargins.Y);
        }

        private void DrawImage()
        {
            Image = new Bitmap((int)Size.X, (int)Size.Y);
            var g = Graphics.FromImage(Image);
            g.FillRectangle(new SolidBrush(Config.UiBackColor), 0, 0, (int)Size.X, (int)Size.Y);
            g.DrawRectangle(Config.OutlinePen, new Rectangle(0, 0, (int)Size.X, (int)Size.Y));
            foreach (var block in m_purchaseOptions)
            {
                g.DrawImage(block.Image, block.Position.X, block.Position.Y);
            }
            g.Dispose();
        }
    }
}
