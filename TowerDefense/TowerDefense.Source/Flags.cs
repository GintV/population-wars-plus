using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefense.Source
{
    public static class Flags
    {
        public enum GuardianClass
        {
            None,
            Archer,
            Wizard
        }

        public enum GuardianType
        {
            None,
            Dark,
            Light,
            Fire,
            Ice
        }
    }
}
