using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using TowerDefense.GameEngine.Transactions;
using TowerDefense.Source;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Guardians.Archers;
using TowerDefense.Source.Monsters;
using static TowerDefense.GameEngine.Constants;
using static TowerDefense.Source.Constants;

namespace TowerDefense.GameEngine
{
    public class GameHandler
    {
        private static readonly Lazy<GameHandler> GameInstance = new Lazy<GameHandler>(() => new GameHandler());
        private readonly CancellationTokenSource m_gameTaskCancellationTokenSource = new CancellationTokenSource();

        protected ICoinTransactionController CoinTransactionController { get; set; }
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
            CoinTransactionController = new CoinTransactionController(GameEnvironment);
            GameControls = new GameControls(CoinTransactionController, configuration, GameEnvironment);
            GameEngineSettings.GameCyclesPerSecond = CyclesPerSecond;
            SetConfiguration(configuration);
        }

        public void RunGame()
        {
            GameEnvironment.Inventory.Coins.Set(441288);
            GameEnvironment.Tower.GuardianSpace.AddBlock();
            GameEnvironment.Inventory.Guardians.Add(new DarkArcher());
            GameEnvironment.Inventory.Guardians.Add(new LightArcher());
            Task.Factory.StartNew(() =>
            {
                var i = 0;
                var lastFrameTime = DateTime.Now;
                while (!m_gameTaskCancellationTokenSource.Token.IsCancellationRequested)
                {
                    var currentTime = DateTime.Now;
                    var delta = (currentTime - lastFrameTime).Milliseconds;

                    CoinTransactionController.ExecutePendingTransactions();
                    foreach (var towerBlock in GameEnvironment.Tower.GuardianSpace.TowerBlocks)
                    {
                        //towerBlock.Guardian?.Attack();
                    }
                    foreach (var monster in GameEnvironment.Monsters.ToList())
                    {
                        monster.Move(delta);
                        if (monster.Location.X < 400)
                        {
                            GameEnvironment.Monsters.Remove(monster);
                        }
                    }
                    Renderer?.Render(GameEnvironment.Tower, GameEnvironment.Monsters, GameEnvironment.Projectiles.OfType<Projectile>());
                    if (++i == 50)
                    {
                        GameEnvironment.Monsters.Add(new Bubble(1, new Vector2(1600, 550), 10));
                        GameEnvironment.Inventory.Coins.Set(GameEnvironment.Inventory.Coins.Get() + 1);
                        GameEnvironment.Tower.HealthPointsRemaining.Set(GameEnvironment.Tower.HealthPointsRemaining.Get() + 1);
                        GameEnvironment.Tower.ManaPointsRemaining.Set(GameEnvironment.Tower.ManaPointsRemaining.Get() + 1);
                        i = 0;
                    }
                    lastFrameTime = currentTime;
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
