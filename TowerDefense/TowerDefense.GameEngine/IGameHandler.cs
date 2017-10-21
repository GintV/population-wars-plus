using System;
using System.Threading;
using System.Threading.Tasks;
using TowerDefense.Source;

namespace TowerDefense.GameEngine
{
    public interface IGameHandler
    {
        IGameControls GameControls { get; }
        IGameEnvironment GameEnvironment { get; }
        void CreateGame(IConfiguration configuration);
        void RunGame();
    }

    public class GameHandler : IGameHandler
    {
        private static readonly Lazy<GameHandler> GameInstance = new Lazy<GameHandler>(() => new GameHandler());

        public IConfiguration Configuration { get; private set; }
        public IGameEnvironment GameEnvironment { get; private set; }
        public IGameControls GameControls { get; private set; }

        public static IGameHandler GetHandler() => GameInstance.Value;

        public void CreateGame(IConfiguration configuration)
        {
            Configuration = configuration;
            GameEnvironment = new GameEnvironment();
            GameControls = new GameControls(configuration, GameEnvironment);
        }

        public void RunGame()
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var towerBlock in GameEnvironment.Tower.GuardianSpace.TowerBlocks)
                {
                    towerBlock.Guardian.Attack();
                }

                Thread.Sleep(16);
            });
        }
    }
}
