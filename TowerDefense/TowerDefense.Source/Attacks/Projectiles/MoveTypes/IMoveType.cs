using System;
using System.Numerics;

namespace TowerDefense.Source.Attacks.Projectiles.MoveTypes
{
    public interface IMoveType
    {
        bool Move(ref Vector2 currentLocation, Vector2 targetLocation, int speed);
    }
}
