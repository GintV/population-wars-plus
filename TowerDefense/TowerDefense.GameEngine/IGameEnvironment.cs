using System.Collections.Generic;
using TowerDefense.GameEngine.Transactions;
using TowerDefense.Source;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Monsters;

namespace TowerDefense.GameEngine
{
    public interface IGameEnvironment
    {
        GameInfo GameInfo { get; }
        InventoryInfo InventoryInfo { get; }
        Inventory Inventory { get; }
        List<Monster> Monsters { get; }
        List<Projectile> Projectiles { get; }
        Tower Tower { get; }
    }

    internal class GameEnvironment : IGameEnvironment
    {
        public GameInfo GameInfo { get; }
        public InventoryInfo InventoryInfo { get; }
        public Inventory Inventory { get; }
        public List<Monster> Monsters { get; }
        public List<Projectile> Projectiles { get; }
        public Tower Tower { get; }

        public GameEnvironment()
        {
            Inventory = new Inventory();
            Monsters = new List<Monster>();
            Projectiles = new List<Projectile>();
            Tower = new Tower();
            GameInfo = new GameInfoProvider(Inventory.Coins, Tower.HealthPointsRemaining, Tower.HealthPoints, Tower.ManaPointsRemaining, Tower.ManaPoints);
            InventoryInfo = new InventoryInfoProvider(Inventory);
        }
    }
}
