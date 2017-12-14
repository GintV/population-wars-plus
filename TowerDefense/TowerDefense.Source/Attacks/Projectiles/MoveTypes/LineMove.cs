using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Attacks.Projectiles.MoveTypes
{
    public class LineMove : MoveType
    {                
        public override Vector2 Move(Vector2 location, long dt)
        {
            var trajectory = Target - location;
            var distance = Math.Abs(trajectory.Length());

            location += Vector2.Normalize(trajectory) * (float)SourceSpeed;
            return location;
        }
        
    }
}
