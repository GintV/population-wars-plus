using static TowerDefense.Source.Flags;

namespace TowerDefense.Source.Guardians
{
    public static class GuardianFactoryProvider
    {
        public static Maybe<GuardianFactory> GetFactory(GuardianClass guardianClass) => guardianClass == GuardianClass.Archer ?
            (GuardianFactory)ArcherFactory.GetFactory() : guardianClass == GuardianClass.Wizard ? WizardFactory.GetFactory() : null;
    }
}
