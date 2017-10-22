using System.Collections.Generic;
using TowerDefense.Source;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Monsters;

namespace TowerDefense.GameEngine
{
    public interface IGameEnvironment
    {
        Inventory Inventory { get; }
        List<IMonster> Monsters { get; }
        List<IProjectile> Projectiles { get; }
        Tower Tower { get; }
        
    }

    internal class GameEnvironment : IGameEnvironment
    {
        public Inventory Inventory { get; }
        public List<IMonster> Monsters { get; }
        public List<IProjectile> Projectiles { get; }
        public Tower Tower { get; }

        public GameEnvironment()
        {
            Inventory = new Inventory();
            Monsters = new List<IMonster>();
            Projectiles = new List<IProjectile>();
            Tower = new Tower();
        }
    }
}
