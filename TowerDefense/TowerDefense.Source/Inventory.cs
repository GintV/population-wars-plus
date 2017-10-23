using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source.Guardians;
using TowerDefense.Source.Utils;

namespace TowerDefense.Source
{
    public class Inventory
    {
        public Observable<int> Coins { get; private set; }
        public List<Guardian> Guardians { get; set; }

        public Inventory()
        {
            Coins = new Observable<int>();
            Coins.Set(0);
            Guardians = new List<Guardian>();
        }
    }
}
