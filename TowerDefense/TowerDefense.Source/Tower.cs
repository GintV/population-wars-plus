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
        public int UpgradeCost { get; private set; }


        public Tower()
        {
            GuardianSpace = new GuardianSpace();
            HealthPoints = HealthPointsRemaining = 250;
            ManaPoints = ManaPointsRemaining = 50;
            Level = 1;
            UpgradeCost = 100;
        }

        public void Upgrade()
        {
            HealthPoints += 250;
            HealthPointsRemaining += 250;
            ManaPoints += 250;
            ManaPointsRemaining += 50;
            Level += 1;
            var upgradePrice = UpgradeCost * 1.5;
            UpgradeCost = (int)upgradePrice;

            if (Level % 20 == 0)
                GuardianSpace.AddBlock();
        }
    }
}
