namespace TowerDefense.Source
{
    public class Tower
    {
        public GuardianSpace GuardianSpace { get; }
        public int HealthPoints { get; private set; }
        public int HealthPointsRemaining { get; private set; }
        public int Level { get; private set; }
        public int ManaPoints { get; private set; }
        public int ManaPointsRemaining { get; private set; }
        public int UpgradePrice { get; private set; }


        public Tower()
        {
            GuardianSpace = new GuardianSpace();
            HealthPoints = HealthPointsRemaining = 250;
            ManaPoints = ManaPointsRemaining = 50;
            Level = 1;
            UpgradePrice = 100;
        }

        public void Upgrade()
        {
            HealthPoints += 250;
            HealthPointsRemaining += 250;
            ManaPoints += 50;
            ManaPointsRemaining += 250;
            Level += 1;
            var upgradePrice = UpgradePrice * 1.5;
            UpgradePrice = (int)upgradePrice;

            if (Level % 20 == 0)
                GuardianSpace.AddBlock();
        }
    }
}
