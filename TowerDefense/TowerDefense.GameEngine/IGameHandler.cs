using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TowerDefense.Source;
using TowerDefense.Source.Attacks.Projectiles;
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
        void StopGame();
    }

    public class GameHandler : IGameHandler
    {
        private static readonly Lazy<GameHandler> GameInstance = new Lazy<GameHandler>(() => new GameHandler());
        private readonly CancellationTokenSource m_gameTaskCancellationTokenSource = new CancellationTokenSource();
        public IRenderer Renderer { get; set; }


        public IConfiguration Configuration { get; private set; }
        public IGameEnvironment GameEnvironment { get; private set; }
        public IGameControls GameControls { get; private set; }

        private GameHandler() { }

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
            GameEnvironment.Inventory.Coins.Set(441288);
            Task.Factory.StartNew(() =>
            {
                var i = 0;
                while (!m_gameTaskCancellationTokenSource.Token.IsCancellationRequested)
                {
                    GameEnvironment.TransactionController.ExecuteTransactions();
                    foreach (var towerBlock in GameEnvironment.Tower.GuardianSpace.TowerBlocks)
                    {
                        //towerBlock.Guardian?.Attack();
                    }
                    Renderer.Render(GameEnvironment.Tower, GameEnvironment.Monsters, GameEnvironment.Projectiles.OfType<Projectile>());
                    if (++i == 10)
                    {
                        GameEnvironment.Inventory.Coins.Set(GameEnvironment.Inventory.Coins.Get() + 1);
                        GameEnvironment.Tower.HealthPointsRemaining.Set(GameEnvironment.Tower.HealthPointsRemaining.Get() + 1);
                        GameEnvironment.Tower.ManaPointsRemaining.Set(GameEnvironment.Tower.ManaPointsRemaining.Get() + 1);
                        i = 0;
                    }
                    Thread.Sleep(10);
                }
            }, m_gameTaskCancellationTokenSource.Token);
        }

        public void StopGame()
        {
            m_gameTaskCancellationTokenSource.Cancel();
        }
    }
}
