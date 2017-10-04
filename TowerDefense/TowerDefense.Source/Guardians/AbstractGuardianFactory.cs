﻿namespace TowerDefense.Source.Guardians
{
    public abstract class AbstractGuardianFactory 
    {
        public static Maybe<IGuardian> CreateGuardian(GuardianType guardianType)
        {
            var factory = GetConcreteFactory(guardianType);
            if (factory.HasValue)
                return factory.Value.CreateGuardian(guardianType);
            return null;
        }

        private static Maybe<IGuardianFactory> GetConcreteFactory(GuardianType guardianType) =>
            new Maybe<IGuardianFactory>(guardianType.Class == GuardianClass.Archer ? (IGuardianFactory)ArcherFactory.GetFactory() :
            guardianType.Class == GuardianClass.Wizard ? WizardFactory.GetFactory() : null);
            
    }
}