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
            guardianType.Specialization == GuardianSpecialization.Fire ? (Wizard)new FireWizard() : 
            guardianType.Specialization == GuardianSpecialization.Ice ? new IceWizard() : null;
    }
}
