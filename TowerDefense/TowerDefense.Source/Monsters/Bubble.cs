using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Monsters
{
    internal class Bubble : Monster
    {
        public override int HealthPoints { get; }
        public override int HealthPointsRemaining { get; }
        public override Vector2 Location { get; }
        public override int Speed { get; }
    }
}
