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
        IInventory Inventory { get; }
        ICollection<Monster> Monsters { get; }
        ICollection<Projectile> Projectiles { get; }
        ITower Tower { get; }
    }

    internal class GameEnvironment : IGameEnvironment
    {
        public GameInfo GameInfo { get; }
        public InventoryInfo InventoryInfo { get; }
        public IInventory Inventory { get; }
        public ICollection<Monster> Monsters { get; }
        public ICollection<Projectile> Projectiles { get; }
        public ITower Tower { get; }

        public GameEnvironment(IInventory inventory, ICollection<Monster> monsters, ICollection<Projectile> projectiles, ITower tower,
            GameInfo gameInfo, InventoryInfo inventoryInfo)
        {
            Inventory = inventory;
            Monsters = monsters;
            Projectiles = projectiles;
            Tower = tower;
            GameInfo = new GameInfoProvider(Inventory.Coins, Tower.HealthPointsRemaining, Tower.HealthPoints, Tower.ManaPointsRemaining, Tower.ManaPoints);
            InventoryInfo = new InventoryInfoProvider(Inventory);
        }
    }
}
