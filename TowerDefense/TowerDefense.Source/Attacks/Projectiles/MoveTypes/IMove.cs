using System;
using System.Numerics;

namespace TowerDefense.Source.Attacks.Projectiles.MoveTypes
{
    public interface IMove : ICloneable
    {
        /// <summary>Moves object with defined fashion.</summary>
        /// <returns>Object location after move.</returns>
        Vector2 Move(Vector2 location);
        void Initialize(Vector2 source, double sourceSpeed, Vector2 target, double targetSpeed);
    }

    public abstract class MoveType : IMove
    {
        public Vector2 Source { get; set; }
        public double SourceSpeed { get; set; }
        public Vector2 Target { get; set; }
        public double TargetSpeed { get; set; }

        public object Clone() => MemberwiseClone();
        public abstract Vector2 Move(Vector2 location);

        public virtual void Initialize(Vector2 source, double sourceSpeed, Vector2 target, double targetSpeed)
        {
            Source = source;
            SourceSpeed = sourceSpeed;
            Target = target;
            TargetSpeed = targetSpeed;
        }

    }
}
