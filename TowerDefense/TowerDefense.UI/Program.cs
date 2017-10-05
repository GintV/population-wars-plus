using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            var gameView = GameView.GetInstance();
            var game = new Game();
            gameView.Game = game;
            gameView.GameLoopThread = new Thread(game.GameLoop);
            gameView.GameLoopThread.Start();
            Application.Run(gameView);
        }
    }
}
