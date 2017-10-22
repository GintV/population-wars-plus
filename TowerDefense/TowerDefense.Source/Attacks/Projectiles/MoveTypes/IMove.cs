using System;
using System.Numerics;

namespace TowerDefense.Source.Attacks.Projectiles.MoveTypes
{
    public interface IMove : ICloneable
    {
        /// <summary>Moves object with defined fashion.</summary>
        /// <returns>Object location after move.</returns>
        Vector2 Move();
        void Initialize(Vector2 source, double sourceSpeed, Vector2 target, double targetSpeed);
    }

    internal abstract class MoveType : IMove
    {
        protected Vector2 Source { get; set; }
        protected double SourceSpeed { get; set; }
        protected Vector2 Target { get; set; }
        protected double TargetSpeed { get; set; }

        public abstract object Clone();
        public abstract Vector2 Move();

        public void Initialize(Vector2 source, double sourceSpeed, Vector2 target, double targetSpeed)
        {
            Source = source;
            SourceSpeed = sourceSpeed;
            Target = target;
            TargetSpeed = targetSpeed;
        }

    }
}
