using System.Numerics;
using TowerDefense.Source.Attacks.Projectiles.MoveTypes;

namespace TowerDefense.Source.Attacks.Projectiles
{
    public class Arrow : Projectile
    {
        public sealed override IMove MoveType { get; set; }

        public Arrow(int collisionDamage = Constants.ProjectileDamageBase.Arrow, double speed = Constants.ProjectileSpeedBase.Arrow) : base(collisionDamage, speed)
        {
            MoveType = new LineMove();
        }

        public override object Clone()
        {
            var arrow = (Arrow)MemberwiseClone();
            arrow.MoveType = (IMove)MoveType.Clone();
            return arrow;
        }

        public override void Upgrade()
        {
            CollisionDamage = (int)(CollisionDamage * Constants.ProjectileDamageMultiplier.Arrow);
        }
    }
}
