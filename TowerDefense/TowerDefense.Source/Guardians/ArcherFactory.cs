using System;
using TowerDefense.Source.Guardians.Archers;

namespace TowerDefense.Source.Guardians
{
    internal class ArcherFactory : IGuardianFactory
    {
        private static readonly Lazy<ArcherFactory> m_factory = new Lazy<ArcherFactory>();

        private ArcherFactory() { }

        public static ArcherFactory GetFactory() =>
            m_factory.Value;

        public Maybe<IGuardian> CreateGuardian(GuardianType guardianType) =>
            guardianType.Specialization == GuardianSpecialization.Dark ? DarkArcher.CreateArcher() :
            guardianType.Specialization == GuardianSpecialization.Light ? LightArcher.CreateArcher() : null;
    }
}