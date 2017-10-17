using static TowerDefense.Source.Flags;

namespace TowerDefense.Source.Guardians
{
    public abstract class GuardianFactory 
    {
        public abstract Maybe<Guardian> CreateGuardian(GuardianType guardianType);
    }
}
