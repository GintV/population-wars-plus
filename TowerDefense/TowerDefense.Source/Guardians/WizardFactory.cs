using System;
using TowerDefense.Source.Guardians.Wizards;

namespace TowerDefense.Source.Guardians
{
    internal class WizardFactory : IGuardianFactory
    {
        private static readonly Lazy<WizardFactory> m_factory = new Lazy<WizardFactory>();

        private WizardFactory() { }

        public static WizardFactory GetFactory() =>
            m_factory.Value;

        public Maybe<IGuardian> CreateGuardian(GuardianType guardianType) => 
            guardianType.Specialization == GuardianSpecialization.Fire ? FireWizard.CreateWizard() :
            guardianType.Specialization == GuardianSpecialization.Ice ? IceWizard.CreateWizard() : null;
    }
}
