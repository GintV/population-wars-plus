using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source.Guardians;

namespace TowerDefense.Source
{
    internal class Inventory
    {
        public int Coins { get; set; }
        public List<IGuardian> Guardians { get; set; }

        public Inventory()
        {
            Coins = 0;
            Guardians = new List<IGuardian>();
        }
    }
}
