namespace TowerDefense.Source
{
    public class GuardianSpace
    {
        public GuardianSlot[] GuardianSlots { get; private set; }
        public int Slots { get; }

        public GuardianSpace()
        {
            GuardianSlots = new[] { new GuardianSlot(), new GuardianSlot(), new GuardianSlot() };
            Slots = 3;
        }
    }
}