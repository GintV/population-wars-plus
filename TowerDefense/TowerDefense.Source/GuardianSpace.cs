using System.Collections.Generic;
using System.Linq;

namespace TowerDefense.Source
{
    public class GuardianSpace
    {
        public TowerBlock[] TowerBlocks { get; private set; }
        public int Blocks { get; private set; }

        public GuardianSpace()
        {
            TowerBlocks = new[] { new TowerBlock(1), new TowerBlock(2), new TowerBlock(3) };
            Blocks = 3;
        }

        public void AddBlock()
        {
            var towerBlocks = TowerBlocks.ToList();
            towerBlocks.Add(new TowerBlock(++Blocks));
            TowerBlocks = towerBlocks.ToArray();
        }
    }
}