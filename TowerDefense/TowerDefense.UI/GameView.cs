using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TowerDefense.UI.Properties;

namespace GUI
{
    public partial class GameView : Form
    {
        private Bitmap m_bitmap;
        private Game m_game;
        private readonly object m_bitmapLock = new object();

        public Thread GameLoopThread { get; set; }

        public Game Game
        {
            set
            {
                m_game = value;
                m_bitmap = new Bitmap((int)value.Boundries.X, (int)value.Boundries.Y);
            }
        }

        public GameView()
        {
            InitializeComponent();
        }

        private void GameViewPaint(object sender, PaintEventArgs e)
        {
            if (m_bitmap == null) return;
            e.Graphics.DrawImage(m_bitmap, 10, 10);
        }

        private void GameViewKeyDown(object sender, KeyEventArgs e)
        {

        }

        public void Redraw(IEnumerable<IRenderable> renderableObjects)
        {
            if (m_bitmap == null) return;
            lock (m_bitmapLock)
            {
                var g = Graphics.FromImage(m_bitmap);
                g.Clear(Color.Transparent);
                foreach (var obj in renderableObjects)
                {
                    g.DrawImage(obj.Image, obj.Position.X, obj.Position.Y, obj.Size.X, obj.Size.Y);
                }
                g.Dispose();
            }
            Invoke((MethodInvoker)Refresh);
            //Invalidate();
        }

        private void GameView_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
