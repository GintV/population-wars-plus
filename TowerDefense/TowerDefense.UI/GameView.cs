using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerDefense.UI
{
    public partial class GameView : Form, IView
    {
        private static GameView s_instance;
        private Bitmap m_bitmap;
        private Game m_game;
        private readonly object m_bitmapLock = new object();
        private ISelectable m_currentlySelected;
        private readonly Pen m_selectionPen;
        public ICollection<IClickable> RegisteredClickables { get; }

        private GameView()
        {
            InitializeComponent();
            RegisteredClickables = new List<IClickable>();
            m_selectionPen = new Pen(Color.SteelBlue, 4);
        }

        public static GameView GetInstance()
        {
            return s_instance ?? (s_instance = new GameView());
        }

        public Thread GameLoopThread { get; set; }

        public Game Game
        {
            set
            {
                m_game = value;
                m_bitmap = new Bitmap((int)value.Boundries.X, (int)value.Boundries.Y);
            }
        }

        private void GameViewPaint(object sender, PaintEventArgs e)
        {
            if (m_bitmap == null) return;
            e.Graphics.DrawImage(m_bitmap, 10, 10);
        }

        private void GameViewKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'c':
                    lock (m_bitmapLock)
                    {
                        m_game.ConstructEnemy();
                    }
                    break;
                case 's':
                    lock (m_bitmapLock)
                    {
                        m_game.ConstructProjectile();
                    }
                    break;
            }
        }

        private void GameViewMouseClick(object sender, MouseEventArgs e)
        {
            foreach (var clickable in RegisteredClickables)
            {
                if (!IsClicked(clickable, e)) continue;
                if (clickable is ISelectable selectable)
                {
                    if (selectable == m_currentlySelected) return;
                    m_currentlySelected?.OnDeselect();
                    m_currentlySelected = selectable;
                }
                clickable.OnClick(new Vector2(e.X - clickable.Position.X, e.Y - clickable.Position.Y));
                return;
            }
            m_currentlySelected?.OnDeselect();
            m_currentlySelected = null;
            //Debug.Write("X: " + e.X + " Y: " + e.Y + "\n");
            //Debug.Write("dX: " + (e.X - 10) + " dY: " + (e.Y - 100) + "\n");
        }

        private static bool IsClicked(IRenderable clickable, MouseEventArgs e)
        {
            return clickable.Position.X <= e.X && clickable.Position.Y <= e.Y &&
                   clickable.Position.X + clickable.Size.X >= e.X && clickable.Position.Y + clickable.Size.Y >= e.Y;
        }

        public void Render(IEnumerable<IRenderable> renderables)
        {
            if (m_bitmap == null) return;
            lock (m_bitmapLock)
            {
                var g = Graphics.FromImage(m_bitmap);
                g.Clear(Color.Transparent);
                foreach (var renderable in renderables)
                {
                    g.DrawImage(renderable.Image, renderable.Position.X, renderable.Position.Y, renderable.Size.X, renderable.Size.Y);
                }
                foreach (var clickable in RegisteredClickables)
                {
                    g.DrawImage(clickable.Image, clickable.Position.X, clickable.Position.Y, clickable.Size.X, clickable.Size.Y);
                }
                // TODO: check boundries
                if (m_currentlySelected != null)
                    g.DrawRectangle(m_selectionPen, m_currentlySelected.Position.X - 3, m_currentlySelected.Position.Y - 3,
                        m_currentlySelected.Size.X + 5, m_currentlySelected.Size.Y + 5);
                g.Dispose();
            }
            Invoke((MethodInvoker)Refresh);
        }
    }
}
