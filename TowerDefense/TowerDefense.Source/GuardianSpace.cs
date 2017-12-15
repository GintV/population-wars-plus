using System.Linq;

namespace TowerDefense.Source
{
    public class GuardianSpace
    {
        public TowerBlock[] TowerBlocks { get; private set; }
        public int Blocks { get; private set; }

        public GuardianSpace()
        {
            TowerBlocks = new[] { new TowerBlock(1), new TowerBlock(2) };
            Blocks = 2;
        }

        public void AddBlock()
        {
            var towerBlocks = TowerBlocks.ToList();
            towerBlocks.Add(new TowerBlock(++Blocks));
            TowerBlocks = towerBlocks.ToArray();
        }

        public void RemoveBlock()
        {
            var towerBlocks = TowerBlocks.ToList();
            towerBlocks.Remove(towerBlocks[towerBlocks.Count - 1]);
            Blocks--;
            TowerBlocks = towerBlocks.ToArray();
        }
    }
}