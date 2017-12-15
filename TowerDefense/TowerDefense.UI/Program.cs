using System;
using System.Threading;
using System.Windows.Forms;
using TowerDefense.GameEngine;
using TowerDefense.Source;
using TowerDefense.UI.MockEngine;

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
            //var game = new Game2();
            //var game = new Game();
            GameHandler.GetHandler().CreateGame(new Configuration(10, 100, 200, 125, 300, 50));
            GameHandler.GetHandler().Renderer = new RendererToViewAdapter(ViewFactory.CreateView(ViewType.NewGameView, GameHandler.GetHandler().GameEnvironment.GameInfo));
            GameHandler.GetHandler().InitGame();
            GameHandler.GetHandler().StartGame();
            gameView.Game = GameHandler.GetHandler();
            Application.Run(gameView);
        }
    }

    internal class Configuration : IConfiguration
    {
        public int BaseGuardianCreationCost { get; }
        public int DistanceToTower { get; }
        public int TowerBaseHeight { get; }
        public int TowerBlockHeight { get; }
        public int TowerWidth { get; }
        public int GuardianShootingHeightInBlock { get; }

        public Configuration(int baseGuardianCreationCost, int distanceToTower, int towerBaseHeight, int towerBlockHeight, int towerWidth, int shootingHeight)
        {
            BaseGuardianCreationCost = baseGuardianCreationCost;
            DistanceToTower = distanceToTower;
            TowerBaseHeight = towerBaseHeight;
            TowerBlockHeight = towerBlockHeight;
            TowerWidth = towerWidth;
            GuardianShootingHeightInBlock = shootingHeight;
        }

    }
}
