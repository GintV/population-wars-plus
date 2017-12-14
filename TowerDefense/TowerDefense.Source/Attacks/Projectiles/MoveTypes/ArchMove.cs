using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Attacks.Projectiles.MoveTypes
{
    public class ArchMove : MoveType
    {
        private Vector2 lineLocation;

        public ArchMove() { }

        private float ArchHelper(float fullDistance, float distance)
        {
            double relativePosition = 1 - distance / fullDistance;

            return (float)(Math.Sin(relativePosition * Math.PI) * 15);
        }


        public override Vector2 Move(Vector2 location, long dt)
        {
            var fullTrajectory = Target - Source;
            var fullDistance = Math.Abs(fullTrajectory.Length());

            var trajectory = Target - lineLocation;
            var distance = Math.Abs(trajectory.Length());

            lineLocation += Vector2.Normalize(trajectory) * (float)SourceSpeed;
            location = lineLocation;

            trajectory = Target - lineLocation;
            distance = Math.Abs(trajectory.Length());

            location.Y += ArchHelper(fullDistance, distance);

            return location;
        }
        
        public override void Initialize(Vector2 source, double sourceSpeed, Vector2 target, double targetSpeed)
        {
            base.Initialize(source, sourceSpeed, target, targetSpeed);
            lineLocation = source;
        }
    }
}
