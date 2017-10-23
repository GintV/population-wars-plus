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
    public class GameHandler
    {
        private static readonly Lazy<GameHandler> GameInstance = new Lazy<GameHandler>(() => new GameHandler());
        private readonly CancellationTokenSource m_gameTaskCancellationTokenSource = new CancellationTokenSource();

        public IConfiguration Configuration { get; private set; }
        public IGameEnvironment GameEnvironment { get; private set; }
        public IGameControls GameControls { get; private set; }
        public IRenderer Renderer { get; set; }


        private GameHandler() { }

        public static GameHandler GetHandler() => GameInstance.Value;

        public void CreateGame(IConfiguration configuration)
        {
            Configuration = configuration;
            GameEnvironment = new GameEnvironment();
            GameControls = new GameControls(configuration, GameEnvironment);
            GameEngineSettings.GameCyclesPerSecond = CyclesPerSecond;
            SetConfiguration(configuration);
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

        private void SetConfiguration(IConfiguration configuration) // TODO: please later refactor this away :)))
        {
            ConfigurationSettings.BaseGuardianCreationCost = configuration.BaseGuardianCreationCost;
            ConfigurationSettings.DistanceToTower = configuration.DistanceToTower;
            ConfigurationSettings.GuardianShootingHeightInBlock = configuration.GuardianShootingHeightInBlock;
            ConfigurationSettings.TowerBaseHeight = configuration.TowerBaseHeight;
            ConfigurationSettings.TowerBlockHeight = configuration.TowerBlockHeight;
            ConfigurationSettings.TowerWidth = configuration.TowerWidth;
        }
    }
}
