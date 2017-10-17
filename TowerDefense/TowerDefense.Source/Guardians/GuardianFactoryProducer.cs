using static TowerDefense.Source.Flags;

namespace TowerDefense.Source.Guardians
{
    public class GuardianFactoryProducer
    {
        public static Maybe<GuardianFactory> GetFactory(GuardianClass guardianClass) => guardianClass == GuardianClass.Archer ?
            (GuardianFactory)ArcherFactory.GetFactory() : guardianClass == GuardianClass.Wizard ? WizardFactory.GetFactory() : null;
    }
}
