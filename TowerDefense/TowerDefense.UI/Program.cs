using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI;

namespace TowerDefense.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var gameView = new GameView();
            var game = new Game(gameView);
            gameView.Game = game;
            gameView.GameLoopThread = new Thread(game.GameLoop);
            gameView.GameLoopThread.Start();
            Application.Run(gameView);
        }
    }
}
