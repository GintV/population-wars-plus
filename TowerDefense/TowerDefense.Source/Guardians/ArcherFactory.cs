using System;
using TowerDefense.Source.Guardians.Archers;
using static TowerDefense.Source.Flags;
using static TowerDefense.Source.Flags.GuardianType;

namespace TowerDefense.Source.Guardians
{
    internal class ArcherFactory : GuardianFactory
    {
        private static readonly Lazy<ArcherFactory> Factory = new Lazy<ArcherFactory>();

        public static ArcherFactory GetFactory() => Factory.Value;

        public override Maybe<Guardian> CreateGuardian(GuardianType guardianType) => guardianType == Dark ? (Guardian)new DarkArcher() :
            guardianType == Light ? new LightArcher() : null;
    }
}