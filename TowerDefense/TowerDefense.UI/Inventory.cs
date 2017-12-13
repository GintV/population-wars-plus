using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using TowerDefense.UI.Properties;
using TowerDefense.UI.Stylers;

namespace TowerDefense.UI
{
    public sealed class Inventory : ClickableContainer
    {
        private readonly BasicClickable m_leftArrow;
        private readonly BasicClickable m_rightArrow;
        private readonly IClickable m_emptyButton;
        private int m_currentStartingIndex;
        private bool m_hasChanged;
        private Image m_image;

        public override Image Image
        {
            get
            {
                if (m_hasChanged)
                    Draw();
                return m_image;
            }
            set => m_image = value;
        }

        public Inventory(Styler styler, IEnumerable<IClickable> clickables = null) : base(styler)
        {
            Position = Config.InventoryPosition;
            Size = Config.InventorySize;
            m_emptyButton = new Button(new InactiveStyler(), null, Config.InventoryBlockSize);
            m_leftArrow = new BasicClickable(_ => SlideLeft())
            {
                Position = Config.InventoryArrowMargins,
                Size = Config.InventoryArrowSize
            };
            m_rightArrow = new BasicClickable(_ => SlideRight())
            {
                Position = new Vector2(Config.InventoryArrowSize.X + Config.InventoryArrowMargins.X + (Config.InventoryBlockMargins.X + Config.InventoryBlockSize.X) * Config.InventoryItemsInViewLimit, Config.InventoryArrowMargins.Y),
                Size = Config.InventoryArrowSize
            };
            Clickables = clickables?.ToList() ?? new List<IClickable>(Config.InventoryItemsInViewLimit);
            m_currentStartingIndex = 0;
            Draw();
        }

        public void SlideLeft()
        {
            if (m_currentStartingIndex == 0) return;
            m_currentStartingIndex--;
            m_hasChanged = true;

        }

        public void SlideRight()
        {
            if (Clickables.Count < Config.InventoryItemsInViewLimit || m_currentStartingIndex + Config.InventoryItemsInViewLimit == Clickables.Count) return;
            m_currentStartingIndex++;
            m_hasChanged = true;
        }

        public void AddInventoryBlock(IClickable block)
        {
            Clickables.Add(block);
            m_hasChanged = true;
        }

        public bool RemoveInventoryBlock(IClickable block)
        {
            var result = Clickables.Remove(block);
            m_hasChanged = result;
            return result;
        }

        public void ReplaceInventoryBlocks(List<IClickable> blocks)
        {
            Clickables = blocks.ToList();
            m_hasChanged = true;
        }

        public override void OnClick(Vector2 clickPosition)
        {
            if (IsClicked(m_leftArrow.Position, m_leftArrow.Size, clickPosition))
                m_leftArrow.OnClick(clickPosition);
            if (IsClicked(m_rightArrow.Position, m_rightArrow.Size, clickPosition))
                m_rightArrow.OnClick(clickPosition);
            for (var i = 0; i < Config.InventoryItemsInViewLimit && i < Clickables.Count; i++)
            {
                if (IsClicked(BlockPositionByIndex(i), Config.InventoryBlockSize, clickPosition))
                {
                    ((List<IClickable>)Clickables)[m_currentStartingIndex + i].OnClick(clickPosition);
                    return;
                }
            }
        }

        protected override void Draw()
        {
            ResolveArrowAppearance();
            Image = new Bitmap((int)Size.X, (int)Size.Y);
            var g = Graphics.FromImage(m_image);
            Styler.DrawRectangle(g, Vector2.Zero, Size);
            var i = 0;
            foreach (var block in Clickables.Skip(m_currentStartingIndex).Take(Config.InventoryItemsInViewLimit))
            {
                var position = BlockPositionByIndex(i++);
                g.DrawImage(block.Image, position.X, position.Y, Config.InventoryBlockSize.X, Config.InventoryBlockSize.Y);
            }
            for (; i < Config.InventoryItemsInViewLimit; i++)
            {
                var position = BlockPositionByIndex(i);
                g.DrawImage(m_emptyButton.Image, position.X, position.Y, Config.InventoryBlockSize.X, Config.InventoryBlockSize.Y);
            }
            g.DrawImage(m_leftArrow.Image, m_leftArrow.Position.X, m_leftArrow.Position.Y, m_leftArrow.Size.X, m_leftArrow.Size.Y);
            g.DrawImage(m_rightArrow.Image, m_rightArrow.Position.X, m_rightArrow.Position.Y, m_rightArrow.Size.X, m_rightArrow.Size.Y);
            g.Dispose();
            m_hasChanged = false;
        }

        private static bool IsClicked(Vector2 position, Vector2 size, Vector2 click)
        {
            return position.X <= click.X && position.Y <= click.Y &&
                   position.X + size.X >= click.X && position.Y + size.Y >= click.Y;
        }

        private static Vector2 BlockPositionByIndex(int i) => new Vector2(
            Config.InventoryArrowSize.X + Config.InventoryBlockMargins.X * (i + 1) + Config.InventoryBlockSize.X * i,
            Config.InventoryBlockMargins.Y);

        private void ResolveArrowAppearance()
        {
            m_leftArrow.Image = m_currentStartingIndex == 0 ? Resources.arrowLeftInactive : Resources.arrowLeft;
            m_rightArrow.Image =
                Clickables.Count <= Config.InventoryItemsInViewLimit ||
                m_currentStartingIndex + Config.InventoryItemsInViewLimit == Clickables.Count
                    ? Resources.arrowRightInactive
                    : Resources.arrowRight;
        }
    }
}
