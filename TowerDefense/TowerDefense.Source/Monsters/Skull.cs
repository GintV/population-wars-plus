using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Monsters
{
    public class Skull : Monster
    {
        public override int HealthPoints { get; }
        public override int HealthPointsRemaining { get; }
        public override Vector2 Location { get; set; }
        public override int Speed { get; }
    }
}
