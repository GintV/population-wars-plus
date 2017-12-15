using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Attacks.Projectiles.MoveTypes
{
    public class LineMove : MoveType
    {
        private bool _first = true;
        private Vector2 _trajectory = Vector2.Zero;

        public override Vector2 Move(Vector2 location, long dt)
        {
            if (_first)
            {
                _first = false;
                _trajectory = Vector2.Normalize(Target - location);
            }

            location += _trajectory * (float)(SourceSpeed * dt / 1000.0);
            return location;
        }
        
    }
}
