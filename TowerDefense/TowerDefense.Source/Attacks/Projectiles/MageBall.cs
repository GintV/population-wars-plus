using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles.MoveTypes;
using static TowerDefense.Source.Constants;

namespace TowerDefense.Source.Attacks.Projectiles
{
    public class MageBall : Projectile
    {
        public sealed override IMove MoveType { get; set; }

        public MageBall(int collisionDamage = ProjectileDamageBase.MageBall, double speed = ProjectileSpeedBase.MageBall) : base(collisionDamage, speed)
        {
            MoveType = new LineMove();
        }

        public override object Clone()
        {
            var arrow = (MageBall)MemberwiseClone();
            arrow.MoveType = (IMove)MoveType.Clone();
            return arrow;
        }

        public override void Upgrade()
        {
            CollisionDamage = (int)(CollisionDamage * ProjectileDamageMultiplier.MageBall);
            Speed = Speed * ProjectileSpeedMultiplier.MageBall;
        }

        public override void Downgrade()
        {
            CollisionDamage = (int)(CollisionDamage / ProjectileDamageMultiplier.MageBall);
            Speed = Speed / ProjectileSpeedMultiplier.MageBall;
        }
    }
}
