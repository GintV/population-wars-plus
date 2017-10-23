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
    public class NewGameView : IView
    {
        private readonly IView m_renderingView;
        private readonly IGameInfo m_gameInfo;
        private readonly ICollection<IRenderable> m_addedRenderables;
        private bool m_isInstansiated;
        private readonly GameInfoObserver m_gameInfoObserver;
        private Sidebar m_sidebar;

        public NewGameView(IView renderingView, IGameInfo gameInfo)
        {
            m_isInstansiated = false;
            m_addedRenderables = new List<IRenderable>();
            m_renderingView = renderingView;
            m_gameInfo = gameInfo;
            m_gameInfoObserver = new GameInfoObserver(m_gameInfo);
            m_gameInfo.Subscribe(m_gameInfoObserver);
        }

        public void Render(IEnumerable<IRenderable> renderables)
        {
            if (!m_isInstansiated)
                Instantiate();
            m_renderingView.Render(renderables.Concat(m_addedRenderables));
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
            InitSelectables();
        }

        private void InitSelectables()
        {
            for (var i = 0; i < 3; i++)
            {
                m_renderingView.RegisterClickable(new GuardianSlot(RedrawSidebar, RedrawSidebar)
                {
                    Position = new Vector2(300, 100 * (i + 1)),
                    Size = new Vector2(100, 100),
                    Index = i,
                    Guardian = i % 2 == 0 ? new Guardian() : null
                });
            }
        }

        private void RedrawSidebar()
        {
            m_sidebar.ClearButtons();
            var selected = m_renderingView.GetSelectedSelectable();
            if (!(selected is GuardianSlot slot) || slot.IsEmpty) return;
            m_sidebar.AddButton("Charge Shot", () => GameHandler.GetHandler().GameControls.ActivateChargeAttack(slot.Index));
            m_sidebar.AddButton(
                new DependantButton<int>("Promote", Config.SideBarButtonSize, new ClickableStyler(), new InactiveStyler(), m_gameInfoObserver.Coins, t => t > 100)
                {
                    OnClickAction = _ => GameHandler.GetHandler().GameControls.PromoteGuardian(slot.Index)
                });
            m_sidebar.AddButton("Upgrade", () => GameHandler.GetHandler().GameControls.UpgradeGuardian(slot.Index));
            m_sidebar.AddButton("Move to Inv", () => GameHandler.GetHandler().GameControls.MoveGuardianToInventory(slot.Index));
            m_sidebar.AddButton("Sell", () => GameHandler.GetHandler().GameControls.SellGuardian(slot.Index));
        }

        private void InitSidebar()
        {
            m_sidebar = new Sidebar(new GuiBackStyler(), Config.SideBarSize);
            m_renderingView.RegisterClickable(m_sidebar);
            m_addedRenderables.Add(m_sidebar);
        }

        private void InitInventory()
        {
            var inv = new Inventory(new GuiBackStyler());
            m_addedRenderables.Add(inv);
            m_renderingView.RegisterClickable(inv);
        }

        private void InitShop()
        {
            var shop = new Shop(new GuiBackStyler());
            m_addedRenderables.Add(shop);
            m_renderingView.RegisterClickable(shop);
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
    }
}
