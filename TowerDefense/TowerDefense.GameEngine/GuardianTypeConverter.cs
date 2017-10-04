using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source;
using TowerDefense.Source.Guardians;
using static System.Enum;

namespace TowerDefense.GameEngine
{
    internal static class GuardianTypeConverter
    {
        internal static Source.Guardians.GuardianType Convert(GuardianType guardianType) =>
            new Source.Guardians.GuardianType((GuardianClass)Parse(typeof(GuardianClass), guardianType.Class),
            (GuardianSpecialization)Parse(typeof(GuardianSpecialization), guardianType.Specialization));
    }
}
