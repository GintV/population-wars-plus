using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source.Guardians;
using static System.Enum;
using static TowerDefense.Source.Flags;

namespace TowerDefense.GameEngine
{
    internal static class GuardianTypeConverter
    {
        internal static (GuardianClass, GuardianType) Convert(string guardianClass, string guardianType)
        {
            if (!TryParse(guardianClass, true, out GuardianClass enumClass))
                enumClass = GuardianClass.None;
            if (!TryParse(guardianType, true, out GuardianType enumType))
                enumType = GuardianType.None;
            return (enumClass, enumType);
        }
    }
}
