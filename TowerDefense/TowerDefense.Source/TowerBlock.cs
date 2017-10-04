using TowerDefense.Source.Guardians;

namespace TowerDefense.Source
{
    public class TowerBlock
    {
        public int BlockNumber { get; }
        public IGuardian Guardian { get; set; }

        public TowerBlock(int blockNumber)
        {
            BlockNumber = blockNumber;
        }

    }
}