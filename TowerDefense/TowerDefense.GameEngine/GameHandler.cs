using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using TowerDefense.GameEngine.Transactions;
using TowerDefense.Source;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Guardians.Archers;
using TowerDefense.Source.Mediator;
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

        protected ICoinTransactionController CoinTransactionController { get; set; }
        public IConfiguration Configuration { get; private set; }
        public IGameEnvironment GameEnvironment { get; private set; }
        public IGameControls GameControls { get; private set; }
        public IRenderer Renderer { get; set; }
        public Observable<GameState> State { get; }

        public Mediator Mediator { get; set; }

        private GameHandler()
        {
            State = new Observable<GameState>();
            State.Set(GameState.NotCreated);
        }

        public static GameHandler GetHandler() => GameInstance.Value;

        public void CreateGame(IConfiguration configuration)
        {
            Configuration = configuration;

            var inventory = new Inventory();;
            var tower = new Tower();

            GameEnvironment = new GameEnvironment(inventory, new List<Monster>(), new List<Projectile>(), tower,
                new GameInfoProvider(inventory.Coins, tower.HealthPointsRemaining, tower.HealthPoints, tower.ManaPointsRemaining, tower.ManaPoints),
                new InventoryInfoProvider(inventory));
            CoinTransactionController = new CoinTransactionController(GameEnvironment);
            GameControls = new GameControls(CoinTransactionController, configuration, GameEnvironment);
            GameEngineSettings.GameCyclesPerSecond = CyclesPerSecond;
            SetConfiguration(configuration);

            State.Set(GameState.NotStarted);

            Mediator = new Mediator(GameEnvironment.Projectiles, GameEnvironment.Monsters, OnMonsterDestroyed);
        }

        public void InitGame()
        {
            GameEnvironment.Inventory.Guardians.Clear();
            GameEnvironment.Inventory.Coins.Set(1000000000);
            GameEnvironment.Tower.Reset();
        }

        public void StartGame()
        {
            Task.Factory.StartNew(() =>
            {
                var spawnTimer = 0;
                var regenTimer = 0;
                var lastFrameTime = DateTime.Now;
                var rng = new Random();
                while (!m_gameTaskCancellationTokenSource.Token.IsCancellationRequested && GameEnvironment.Tower.HealthPointsRemaining.Get() > 0)
                {
                    var currentTime = DateTime.Now;
                    var delta = (currentTime - lastFrameTime).Milliseconds;

                    CoinTransactionController.ExecutePendingTransactions();

                    if (State.Get() == GameState.Running)
                    {
                        foreach (var towerBlock in GameEnvironment.Tower.GuardianSpace.TowerBlocks)
                        {
                            var mob = GameEnvironment.Monsters.DefaultIfEmpty(new NullMonster()).FirstOrDefault();
                            var projectiles = towerBlock.Guardian?.Attack(mob.Location, mob.Speed, delta);
                            if (projectiles != null)
                            {
                                projectiles.ForEach(projectile =>
                                {
                                    projectile.SetMediator(Mediator);
                                    projectile.Location = new Vector2(400, 900 - (Configuration.TowerBaseHeight +
                                        Configuration.TowerBlockHeight * (towerBlock.BlockNumber - 1) +
                                        Configuration.GuardianShootingHeightInBlock));
                                });
                                projectiles.ForEach(p => GameEnvironment.Projectiles.Add(p));
                                //GameEnvironment.Projectiles.AddRange(projectiles);
                            }
                        }
                        foreach (var monster in GameEnvironment.Monsters.ToList())
                        {
                            monster.Move(delta);
                            if (monster.Location.X < 400)
                            {
                                GameEnvironment.Tower.HealthPointsRemaining.Set(GameEnvironment.Tower.HealthPointsRemaining.Get() - 15);
                                GameEnvironment.Monsters.Remove(monster);
                            }
                        }
                        foreach (var projectile in GameEnvironment.Projectiles.ToList())
                        {
                            projectile.Move(delta);
                            if (projectile.Location.Y < 200 || projectile.Location.X > 1600)
                            {
                                GameEnvironment.Projectiles.Remove(projectile);
                            }
                        }
                        spawnTimer += delta;
                        if (spawnTimer > 2000)
                        {
                            spawnTimer = spawnTimer % 2000;
                            var monster = rng.Next(2) == 0 ? new Bubble(200, new Vector2(1600, 550), 30) : (Monster)new Skull(150, new Vector2(1600, 550), 50);
                            monster.SetMediator(Mediator);
                            GameEnvironment.Monsters.Add(monster);
                            
                        }
                        regenTimer += delta;
                        if (regenTimer > 500)
                        {
                            regenTimer = regenTimer % 500;
                            if (GameEnvironment.Tower.ManaPointsRemaining.Get() < GameEnvironment.Tower.ManaPoints.Get())
                                GameEnvironment.Tower.ManaPointsRemaining.Set(GameEnvironment.Tower.ManaPointsRemaining.Get() + 1);
                            GameEnvironment.Inventory.Coins.Set(GameEnvironment.Inventory.Coins.Get() + 5);
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

        public void OnMonsterDestroyed(Monster monster)
        {
            if (monster is Skull)
            {
                GameEnvironment.Inventory.Coins.Set(GameEnvironment.Inventory.Coins.Get() + 50);
            }
            if (monster is Bubble)
            {
                GameEnvironment.Inventory.Coins.Set(GameEnvironment.Inventory.Coins.Get() + 30);
            }
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
