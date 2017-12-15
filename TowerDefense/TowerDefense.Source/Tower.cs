using TowerDefense.Source.Utils;

namespace TowerDefense.Source
{
    public class Tower
    {
        public GuardianSpace GuardianSpace { get; }
        public Observable<int> HealthPoints { get; }
        public Observable<int> HealthPointsRemaining { get; }
        public int Level { get; private set; }
        public Observable<int> ManaPoints { get; }
        public Observable<int> ManaPointsRemaining { get; }
        public int UpgradeCost { get; private set; }


        public Tower()
        {
            GuardianSpace = new GuardianSpace();
            HealthPoints = new Observable<int>();
            HealthPointsRemaining = new Observable<int>();
            ManaPoints = new Observable<int>();
            ManaPointsRemaining = new Observable<int>();
            HealthPoints.Set(250);
            HealthPointsRemaining.Set(250);
            ManaPoints.Set(50);
            ManaPointsRemaining.Set(50);
            Level = 1;
            UpgradeCost = 100;
        }

        public void Upgrade()
        {
            HealthPoints.Set(HealthPoints.Get() + 250);
            HealthPointsRemaining.Set(HealthPointsRemaining.Get() + 250);
            ManaPoints.Set(ManaPoints.Get() + 50);
            ManaPointsRemaining.Set(ManaPointsRemaining.Get() + 50);
            Level += 1;
            var upgradePrice = UpgradeCost * 1.5;
            UpgradeCost = (int)upgradePrice;

            if (Level % 5 == 0)
                GuardianSpace.AddBlock();
        }

        public void Downgrade(int upgradeCost)
        {
            HealthPoints.Set(HealthPoints.Get() - 250);
            HealthPointsRemaining.Set(HealthPointsRemaining.Get() - 250);
            ManaPoints.Set(ManaPoints.Get() - 50);
            ManaPointsRemaining.Set(ManaPointsRemaining.Get() - 50);
            if (Level % 5 == 0)
                GuardianSpace.RemoveBlock();
            Level -= 1;
            UpgradeCost = upgradeCost;
        }

        public void Reset()
        {
            while (GuardianSpace.Blocks != 0)
            {
                GuardianSpace.RemoveBlock();
            }
            GuardianSpace.AddBlock();
            GuardianSpace.AddBlock();
            HealthPoints.Set(250);
            HealthPointsRemaining.Set(250);
            ManaPoints.Set(50);
            ManaPointsRemaining.Set(25);
            Level = 1;
            UpgradeCost = 100;
        }
    }
}
