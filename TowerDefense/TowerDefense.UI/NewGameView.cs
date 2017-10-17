using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;

namespace TowerDefense.UI
{
    public class NewGameView : IView
    {
        private readonly GameView m_renderingView;
        private readonly Game m_game;
        private readonly ICollection<IRenderable> m_addedRenderables;
        private bool m_isInstansiated;

        public NewGameView(GameView renderingView, Game game)
        {
            m_isInstansiated = false;
            m_addedRenderables = new List<IRenderable>();
            m_renderingView = renderingView;
            m_game = game;
        }

        public void Render(IEnumerable<IRenderable> renderables)
        {
            if (!m_isInstansiated)
                Instantiate();
            m_renderingView.Render(renderables.Concat(m_addedRenderables));
        }

        public void Instantiate()
        {
            if (m_isInstansiated) return;
            m_isInstansiated = true;
            m_renderingView.RegisteredClickables.Clear();
            m_addedRenderables.Add(new InfoBar.InfoBar(m_game));
            InitSidebar();
            InitInventory();
            InitShop();
            ThisNeedsCleanUp();
        }

        private void InitSidebar()
        {
            var sidebar = new Sidebar(Config.SideBarSize)
            {
                Position = new Vector2(0, 0)
            };
            sidebar.AddButton("Charge Shot", () => Debug.WriteLine("charged shot"));
            sidebar.AddButton("Promote", () => Debug.WriteLine("promoted"));
            sidebar.AddButton("Change Spec", () => Debug.WriteLine("changed spec"));
            sidebar.AddButton("Move to Inv", () => Debug.WriteLine("moved to inventory"));
            sidebar.AddButton("Sell", () => Debug.WriteLine("sold"));

            m_renderingView.RegisteredClickables.Add(sidebar);
            m_addedRenderables.Add(sidebar);
        }

        private void InitInventory()
        {
            var inv = new Inventory();
            var ch = 'A';
            m_renderingView.RandomAction = () => inv.AddInventoryBlock(new Button(_ => Debug.WriteLine("Inv button"), Config.InventoryBlockSize) { Description = ch++.ToString() });
            m_addedRenderables.Add(inv);
            m_renderingView.RegisteredClickables.Add(inv);
        }

        private void InitShop()
        {
            var shop = new Shop();
            m_addedRenderables.Add(shop);
            m_renderingView.RegisteredClickables.Add(shop);
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
