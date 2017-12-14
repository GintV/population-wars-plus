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
using TowerDefense.Source.Utils;
using static TowerDefense.GameEngine.Constants;
using static TowerDefense.Source.Constants;

namespace TowerDefense.GameEngine
{
    public class GameHandler
    {
        private static readonly Lazy<GameHandler> GameInstance = new Lazy<GameHandler>(() => new GameHandler());
        private readonly CancellationTokenSource m_gameTaskCancellationTokenSource = new CancellationTokenSource();
        private Task m_task;

        protected ICoinTransactionController CoinTransactionController { get; set; }
        public IConfiguration Configuration { get; private set; }
        public IGameEnvironment GameEnvironment { get; private set; }
        public IGameControls GameControls { get; private set; }
        public IRenderer Renderer { get; set; }

        public Observable<GameState> State { get; }

        private GameHandler()
        {
            State = new Observable<GameState>();
            State.Set(GameState.NotCreated);
        }

        public static GameHandler GetHandler() => GameInstance.Value;

        public void CreateGame(IConfiguration configuration)
        {
            Configuration = configuration;
            GameEnvironment = new GameEnvironment();
            CoinTransactionController = new CoinTransactionController(GameEnvironment);
            GameControls = new GameControls(CoinTransactionController, configuration, GameEnvironment);
            GameEngineSettings.GameCyclesPerSecond = CyclesPerSecond;
            SetConfiguration(configuration);

            State.Set(GameState.NotStarted);
        }

        public void InitGame()
        {
            GameEnvironment.Inventory.Guardians.Clear();
            GameEnvironment.Inventory.Coins.Set(1000);
            GameEnvironment.Tower.Reset();
        }

        public void StartGame()
        {
            m_task = Task.Factory.StartNew(() =>
            {
                var i = 0;
                var lastFrameTime = DateTime.Now;
                while (!m_gameTaskCancellationTokenSource.Token.IsCancellationRequested)
                {
                    var currentTime = DateTime.Now;
                    var delta = (currentTime - lastFrameTime).Milliseconds;

                    CoinTransactionController.ExecutePendingTransactions();

                    if (State.Get() == GameState.Running)
                    {
                        foreach (var towerBlock in GameEnvironment.Tower.GuardianSpace.TowerBlocks)
                        {
                            var mob = GameEnvironment.Monsters.FirstOrDefault();
                            if (mob != null)
                                towerBlock.Guardian?.Attack(mob.Location, mob.Speed, delta);
                        }
                        foreach (var monster in GameEnvironment.Monsters.ToList())
                        {
                            monster.Move(delta);
                            if (monster.Location.X < 400)
                            {
                                GameEnvironment.Monsters.Remove(monster);
                            }
                        }
                        if (++i == 50)
                        {
                            GameEnvironment.Monsters.Add(new Bubble(1, new Vector2(1600, 550), 10));
                            GameEnvironment.Inventory.Coins.Set(GameEnvironment.Inventory.Coins.Get() + 1);
                            GameEnvironment.Tower.HealthPointsRemaining.Set(GameEnvironment.Tower.HealthPointsRemaining.Get() + 1);
                            GameEnvironment.Tower.ManaPointsRemaining.Set(GameEnvironment.Tower.ManaPointsRemaining.Get() + 1);
                            i = 0;
                        }
                    }
                    Renderer?.Render(GameEnvironment.Tower, GameEnvironment.Monsters, GameEnvironment.Projectiles.OfType<Projectile>());
                    lastFrameTime = currentTime;
                    Thread.Sleep(10);
                }
            }, m_gameTaskCancellationTokenSource.Token);
        }

        public void RunGame()
        {
            switch (State.Get())
            {
                case GameState.NotCreated:
                case GameState.Running:
                case GameState.Finished:
                    return;
            }
            State.Set(GameState.Running);
        }

        public void StopGame()
        {
            m_gameTaskCancellationTokenSource.Cancel();
            m_gameTaskCancellationTokenSource.Token.WaitHandle.WaitOne();
        }

        public void PauseGame()
        {
            if (State.Get() != GameState.Running) return;
            State.Set(GameState.Paused);
        }

        public void RestartGame()
        {
            GameEnvironment.Monsters.Clear();
            GameEnvironment.Projectiles.Clear();
            InitGame();
            GameEnvironment.InventoryInfo.OnInventoryChanged();
            State.Set(GameState.NotStarted);
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

    public enum GameState
    {
        NotCreated,
        NotStarted,
        Running,
        Paused,
        Finished
    }
}
