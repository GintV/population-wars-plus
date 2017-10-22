using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TowerDefense.Source.Attacks.Projectiles.MoveTypes
{
    internal class ArchMove : MoveType
    {
        private Vector2 startLocation;
        private Vector2 lineLocation;
        private bool firstMove;


        public ArchMove()
        {
            firstMove = true;
        }

        public bool Move(ref  Vector2 currentLocation, Vector2 targetLocation, int speed)
        {
            if (firstMove) 
            {
                startLocation = new Vector2(currentLocation.X, currentLocation.Y);
                lineLocation = new Vector2(currentLocation.X, currentLocation.Y);
                firstMove = false;
            }

            var fullTrajectory = targetLocation - startLocation;
            var fullDistance = Math.Abs(fullTrajectory.Length());

            var trajectory = targetLocation - lineLocation;
            var distance = Math.Abs(trajectory.Length());
            
            if (distance <= speed)
            {
                currentLocation = targetLocation;
                return true;
            }
            lineLocation += Vector2.Normalize(trajectory) * speed;
            currentLocation = lineLocation;

            trajectory = targetLocation - lineLocation;
            distance = Math.Abs(trajectory.Length());

            currentLocation.Y += ArchHelper(fullDistance, distance);

            return false;
        }

        private float ArchHelper(float fullDistance, float distance)
        {
            double relativePosition = 1 - distance / fullDistance;

            return (float)(Math.Sin(relativePosition * Math.PI)*15);
        }


        public override Vector2 Move()
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
