using System.Linq;

namespace TowerDefense.Source
{
    public class GuardianSpace
    {
        public TowerBlock[] TowerBlocks { get; private set; }
        public int Blocks { get; private set; }

        public GuardianSpace()
        {
            TowerBlocks = new[] { new TowerBlock(1) };
            Blocks = 1;
        }

        public void AddBlock()
        {
            var towerBlocks = TowerBlocks.ToList();
            towerBlocks.Add(new TowerBlock(++Blocks));
            TowerBlocks = towerBlocks.ToArray();
        }
    }
}