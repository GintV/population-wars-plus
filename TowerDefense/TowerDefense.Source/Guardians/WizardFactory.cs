using System;
using TowerDefense.Source.Guardians.Wizards;
using static TowerDefense.Source.Flags;
using static TowerDefense.Source.Flags.GuardianType;

namespace TowerDefense.Source.Guardians
{
    public class WizardFactory : GuardianFactory
    {
        private static readonly Lazy<WizardFactory> Factory = new Lazy<WizardFactory>();

        public static WizardFactory GetFactory() => Factory.Value;

        public override Maybe<Guardian> CreateGuardian(GuardianType guardianType) => guardianType == Fire ? (Guardian)new FireWizard() : 
            guardianType == Ice ? new IceWizard() : null;
    }
}
