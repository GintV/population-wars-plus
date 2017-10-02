namespace TowerDefense.Source
{
    public class Tower
    {
        public GuardianSpace GuardianSpace { get; }
        public int HealthPoints { get; private set; }
        public int Level { get; private set; }
        public int ManaPoints { get; private set; }

        public Tower()
        {
            ManaPoints = 10;
            HealthPoints = 50;
            Level = 1;
            GuardianSpace = new GuardianSpace();
        }

        public void Upgrade() { }
    }
}
