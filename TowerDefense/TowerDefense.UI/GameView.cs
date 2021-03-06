﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using TowerDefense.GameEngine;

namespace TowerDefense.UI
{
    public partial class GameView : Form, IView
    {
        private static GameView s_instance;
        private ISelectable m_currentlySelected;
        private IEnumerable<IRenderable> m_renderables;
        public ICollection<IClickable> RegisteredClickables { get; }
        public Action RandomAction { get; set; }

        private GameView()
        {
            InitializeComponent();
            RegisteredClickables = new List<IClickable>();
        }

        public static GameView GetInstance()
        {
            return s_instance ?? (s_instance = new GameView());
        }

        public GameHandler Game { get; set; }

        private void GameViewPaint(object sender, PaintEventArgs e)
        {
            if (m_renderables == null) return;
            e.Graphics.Clear(Color.Gainsboro);
            foreach (var renderable in m_renderables)
            {
                e.Graphics.DrawImage(renderable.Image, renderable.Position.X, renderable.Position.Y, renderable.Size.X, renderable.Size.Y);
            }
            if (m_currentlySelected != null)
                e.Graphics.DrawRectangle(Config.SelectionPen, m_currentlySelected.Position.X - 3, m_currentlySelected.Position.Y - 3,
                    m_currentlySelected.Size.X + 5, m_currentlySelected.Size.Y + 5);
        }

        private void GameViewKeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            switch (e.KeyChar)
            {
                case 'c':
                    Game.ConstructEnemy();
                    break;
                case 's':
                    Game.ConstructProjectile();
                    break;
                case 'u':
                    Game.DoStuff();
                    break;
                case 't':
                    Game.UpgradeTower();
                    break;
                case 'a':
                    RandomAction?.Invoke();
                    break;
            }
            */
        }

        private void GameViewMouseClick(object sender, MouseEventArgs e)
        {
            foreach (var clickable in RegisteredClickables)
            {
                if (!IsClicked(clickable, e)) continue;
                if (clickable is ISelectable selectable)
                {
                    if (selectable == m_currentlySelected) return;
                    var previousSelection = m_currentlySelected;
                    m_currentlySelected = selectable;
                    previousSelection?.OnDeselect();
                }
                clickable.OnClick(new Vector2(e.X - clickable.Position.X, e.Y - clickable.Position.Y));
                return;
            }
            var lastSelection = m_currentlySelected;
            m_currentlySelected = null;
            lastSelection?.OnDeselect();
        }

        private static bool IsClicked(IRenderable clickable, MouseEventArgs e)
        {
            return clickable.Position.X <= e.X && clickable.Position.Y <= e.Y &&
                   clickable.Position.X + clickable.Size.X >= e.X && clickable.Position.Y + clickable.Size.Y >= e.Y;
        }

        public void Render(IEnumerable<IRenderable> renderables)
        {
            m_renderables = renderables;
            try
            {
                Invoke((MethodInvoker) Refresh);
            }
            catch (ObjectDisposedException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            //Invalidate();
        }

        public void RegisterClickable(IClickable clickable)
        {
            RegisteredClickables.Add(clickable);
        }

        public void DeregisterClickable(IClickable clickable)
        {
            RegisteredClickables.Remove(clickable);
        }

        public void ClearClickables()
        {
            RegisteredClickables.Clear();
        }

        public ISelectable GetSelectedSelectable()
        {
            return m_currentlySelected;
        }
    }
}
