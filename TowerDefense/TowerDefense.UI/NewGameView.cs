using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using TowerDefense.GameEngine;
using TowerDefense.Source.Utils;
using TowerDefense.UI.Stylers;

namespace TowerDefense.UI
{
    public class NewGameView : IView, IIventoryInfoSubscriber
    {
        private readonly IView m_renderingView;
        private readonly GameInfo m_gameInfo;
        private readonly ICollection<IRenderable> m_addedRenderables;
        private bool m_isInstansiated;
        private readonly GameInfoObserver m_gameInfoObserver;
        private Sidebar m_sidebar;
        private Inventory m_inventory;
        private readonly List<GuardianSlot> m_guardianSlots;
        private int? m_selectedGuardianIndex;

        public NewGameView(IView renderingView, GameInfo gameInfo)
        {
            m_isInstansiated = false;
            m_addedRenderables = new List<IRenderable>();
            m_renderingView = renderingView;
            m_gameInfo = gameInfo;
            m_gameInfoObserver = new GameInfoObserver(m_gameInfo);
            m_gameInfo.Subscribe(m_gameInfoObserver);
            m_guardianSlots = new List<GuardianSlot>();
            GameHandler.GetHandler().GameEnvironment.InventoryInfo.Subscribe(this);
        }

        public void Render(IEnumerable<IRenderable> renderables)
        {
            if (!m_isInstansiated)
                Instantiate();
            var renderablesToPass = renderables.ToList();
            var tower = renderablesToPass.OfType<Tower>().Single();
            while (m_guardianSlots.Count < tower.Level)
            {
                var index = m_guardianSlots.Count;
                m_guardianSlots.Add(new GuardianSlot(() => HandleGuardianSelect(index), () => HandleGuardianDeselect(index))
                {
                    Position = new Vector2(313, 600 - 110 * index),
                    Size = new Vector2(100, 100)
                });
                RegisterClickable(m_guardianSlots[index]);
            }
            while (m_guardianSlots.Count > tower.Level)
            {
                var lastIndex = m_guardianSlots.Count - 1;
                DeregisterClickable(m_guardianSlots[lastIndex]);
                m_guardianSlots.RemoveAt(lastIndex);
            }
            UpdateGuardianSlots(renderablesToPass.OfType<Guardian>());
            m_renderingView.Render(renderablesToPass.Where(r => r.GetType() != typeof(Guardian)).Concat(m_addedRenderables).Concat(m_guardianSlots));
        }

        public void RegisterClickable(IClickable clickable)
        {
            m_renderingView.RegisterClickable(clickable);
        }

        public void DeregisterClickable(IClickable clickable)
        {
            m_renderingView.DeregisterClickable(clickable);
        }

        public void ClearClickables()
        {
            m_renderingView.ClearClickables();
        }

        public ISelectable GetSelectedSelectable()
        {
            return m_renderingView.GetSelectedSelectable();
        }

        public void Instantiate()
        {
            if (m_isInstansiated) return;
            m_isInstansiated = true;
            m_renderingView.ClearClickables();
            m_addedRenderables.Add(new InfoBar.InfoBar(new GuiBackStyler(), m_gameInfo));
            InitSidebar();
            InitInventory();
            InitShop();
            ThisNeedsCleanUp();
            //InitSelectables();
        }

        //private void InitSelectables()
        //{
        //    for (var i = 0; i < 3; i++)
        //    {
        //        m_renderingView.RegisterClickable(new GuardianSlot(RedrawSidebar, RedrawSidebar)
        //        {
        //            Position = new Vector2(313, 350 + 110 * (i + 1)),
        //            Size = new Vector2(100, 100),
        //            Index = i,
        //            Guardian = i % 2 == 0 ? new Guardian() : null
        //        });
        //    }
        //}

        private void RedrawSidebar()
        {
            m_sidebar.ClearButtons();
            var selected = m_renderingView.GetSelectedSelectable();
            if (!(selected is GuardianSlot slot) || slot.IsEmpty) return;
            m_sidebar.AddButton("Charge Shot", () => GameHandler.GetHandler().GameControls.ActivateChargeAttack(slot.Guardian.Index));
            m_sidebar.AddButton(
                new DependantButton<int>("Promote", Config.SideBarButtonSize, new ClickableStyler(), new InactiveStyler(), m_gameInfoObserver.Coins, t => t > 100)
                {
                    OnClickAction = _ => GameHandler.GetHandler().GameControls.PromoteGuardian(slot.Guardian.Index)
                });
            m_sidebar.AddButton("Upgrade", () => GameHandler.GetHandler().GameControls.UpgradeGuardian(slot.Guardian.Index));
            m_sidebar.AddButton("Move to Inv", () => GameHandler.GetHandler().GameControls.MoveGuardianToInventory(slot.Guardian.Index));
            m_sidebar.AddButton("Sell", () => GameHandler.GetHandler().GameControls.SellGuardian(slot.Guardian.Index));
        }

        private void InitSidebar()
        {
            m_sidebar = new Sidebar(new GuiBackStyler(), Config.SideBarSize);
            m_renderingView.RegisterClickable(m_sidebar);
            m_addedRenderables.Add(m_sidebar);
        }

        private void InitInventory()
        {
            m_inventory = new Inventory(new GuiBackStyler());
            m_addedRenderables.Add(m_inventory);
            m_renderingView.RegisterClickable(m_inventory);
            OnInventoryChange();
        }

        private void InitShop()
        {
            var shop = new Shop(new GuiBackStyler());
            m_addedRenderables.Add(shop);
            m_renderingView.RegisterClickable(shop);
        }

        private void UpdateGuardianSlots(IEnumerable<Guardian> guardians)
        {
            bool needsRedraw = false;
            var foundIndices = new List<int>();
            foreach (var guardian in guardians)
            {
                foundIndices.Add(guardian.Index);
                if (m_guardianSlots[guardian.Index].Guardian == null || m_guardianSlots[guardian.Index].Guardian.ReprestentedType != guardian.ReprestentedType)
                {
                    m_guardianSlots[guardian.Index].Guardian = guardian;
                    needsRedraw = true;
                }
            }
            foreach (var guardianSlot in m_guardianSlots)
            {
                if (guardianSlot.Guardian != null && !foundIndices.Contains(guardianSlot.Guardian.Index))
                {
                    guardianSlot.Guardian = null;
                    needsRedraw = true;
                }
            }
            if (needsRedraw || m_selectedGuardianIndex != null && !foundIndices.Contains(m_selectedGuardianIndex.Value))
            {
                RedrawSidebar();
            }
        }

        private void HandleGuardianSelect(int i)
        {
            if (i != m_selectedGuardianIndex)
            {
                m_selectedGuardianIndex = i;
                RedrawSidebar();
            }
        }

        private void HandleGuardianDeselect(int i)
        {
            if (m_selectedGuardianIndex == i)
            {
                m_selectedGuardianIndex = null;
                RedrawSidebar();
            }
        }

        private void HandleInventoryClick(int i)
        {
            if (m_selectedGuardianIndex != null)
            {
                GameHandler.GetHandler().GameControls.SwapGuardians(m_selectedGuardianIndex.Value, i);
            }
        }

        // TODO
        private void ThisNeedsCleanUp()
        {
            var tempImage = new Bitmap(222, 54);
            var tempPath = new GraphicsPath();
            tempPath.AddLine(2, 52, 52, 2);
            tempPath.AddLine(52, 2, 220, 2);
            tempPath.AddLine(220, 2, 220, 52);
            tempPath.AddLine(220, 52, 2, 52);
            var g = Graphics.FromImage(tempImage);
            Config.ConfigureGraphics(g);
            g.FillPath(Config.UiBackBrush, tempPath);
            g.DrawPath(Config.NarrowOutlinePen, tempPath);
            var format = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString("Inventory", Config.DefaultFont, Config.TextBrush, new RectangleF(0, 0, 210, 50), format);
            g.Dispose();
            m_addedRenderables.Add(new BasicRenderable { Image = tempImage, Position = new Vector2(578, 726), Size = new Vector2(222, 54) });

            //----------------------------------------------

            tempImage = new Bitmap(222, 54);
            tempPath = new GraphicsPath();
            tempPath.AddLine(2, 52, 2, 2);
            tempPath.AddLine(2, 2, 172, 2);
            tempPath.AddLine(172, 2, 222, 52);
            tempPath.AddLine(222, 52, 2, 52);
            g = Graphics.FromImage(tempImage);
            Config.ConfigureGraphics(g);
            g.FillPath(Config.UiBackBrush, tempPath);
            g.DrawPath(Config.NarrowOutlinePen, tempPath);
            format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString("Shop", Config.DefaultFont, Config.TextBrush, new RectangleF(10, 0, 220, 50), format);
            g.Dispose();
            m_addedRenderables.Add(new BasicRenderable { Image = tempImage, Position = new Vector2(800 - 4, 726), Size = new Vector2(222, 54) });
        }

        public void OnInventoryChange()
        {
            var clickables = new List<IClickable>();
            int i = 0;
            foreach (var guardian in GameHandler.GetHandler().GameEnvironment.Inventory.Guardians.Except(GameHandler.GetHandler().GameEnvironment.Tower.GuardianSpace.TowerBlocks.Select(b => b.Guardian)))
            {
                var index = i;
                clickables.Add(new BasicClickable(_ => { HandleInventoryClick(index); })
                {
                    Image = ImageRepository.GetInstance().GetImage(guardian.GetType())
                });
                i++;
            }
            m_inventory.ReplaceInventoryBlocks(clickables);
        }
    }
}
