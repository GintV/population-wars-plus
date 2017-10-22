using System;
using System.Threading;
using System.Threading.Tasks;
using TowerDefense.Source;
using static TowerDefense.GameEngine.Constants;
using static TowerDefense.Source.Constants;

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
            GameEngineSettings.GameCyclesPerSecond = CyclesPerSecond;
        }

        public void RunGame()
        {
            Task.Factory.StartNew(() =>
            {

                Thread.Sleep(16);
            });
        }
    }
}
