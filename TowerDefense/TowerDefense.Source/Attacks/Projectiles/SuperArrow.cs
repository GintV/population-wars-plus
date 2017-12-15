using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles.MoveTypes;
using TowerDefense.Source.Monsters;
using static TowerDefense.Source.Constants.GameEngineSettings;

namespace TowerDefense.Source.Attacks.Projectiles
{
    public class SuperArrow : Projectile
    {
        public sealed override IMove MoveType { get; set; }

        public SuperArrow(int collisionDamage, double speed) : base(collisionDamage, speed)
        {
            MoveType = new ArchMove();
        }

        public override object Clone()
        {
            var superArrow = (SuperArrow)MemberwiseClone();
            superArrow.MoveType = (IMove)MoveType.Clone();
            return superArrow;
        }

        public override void Upgrade()
        {
            CollisionDamage = (int)(CollisionDamage * Constants.ProjectileDamageMultiplier.SuperArrow);
            Speed = Speed * Constants.ProjectileSpeedMultiplier.SuperArrow;
        }

        public override void Downgrade()
        {
            CollisionDamage = (int)(CollisionDamage / Constants.ProjectileDamageMultiplier.SuperArrow);
            Speed = Speed / Constants.ProjectileSpeedMultiplier.SuperArrow;
        }

        public override void Damage(Skull skull)
        {
            skull.HealthPointsRemaining -= (int)(CollisionDamage * Constants.TypeBasedMultiplier.WeakAgainst);
        }

        public override void Damage(Bubble bubble)
        {
            bubble.HealthPointsRemaining -= (int)(CollisionDamage * Constants.TypeBasedMultiplier.StrongAgainst);
        }
    }
}
