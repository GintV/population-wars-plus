using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source.Guardians;
using TowerDefense.Source.Utils;

namespace TowerDefense.Source
{
    public interface IInventory
    {
        Observable<int> Coins { get; }
        List<Guardian> Guardians { get; set; }
    }

    public class Inventory : IInventory
    {
        public Observable<int> Coins { get; }
        public List<Guardian> Guardians { get; set; }

        public Inventory()
        {
            Coins = new Observable<int>();
            Coins.Set(0);
            Guardians = new List<Guardian>();
        }
    }
}
