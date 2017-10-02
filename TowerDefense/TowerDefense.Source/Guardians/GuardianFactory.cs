namespace TowerDefense.Source.Guardians
{
    internal interface IGuardianFactory
    {
        Maybe<IGuardian> CreateGuard(GuardianType guardianType);
    }

    internal abstract class GuardianFactory
    {
        public static Maybe<IGuardian> CreateGuard(GuardianType guardianType)
        {
            var factory = GetConcreteFactory(guardianType);
            if (factory.HasValue)
                return factory.Value.CreateGuard(guardianType);
            return null;
        }

        private static Maybe<IGuardianFactory> GetConcreteFactory(GuardianType guardianType) =>
            new Maybe<IGuardianFactory>(guardianType.Class == GuardianClass.Archer ? (IGuardianFactory)ArcherFactory.Factory :
            guardianType.Class == GuardianClass.Wizard ? WizardFactory.Factory : null);
            
    }
}
