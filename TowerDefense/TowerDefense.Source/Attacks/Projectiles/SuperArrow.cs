using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles.MoveTypes;
using static TowerDefense.Source.Constants.GameEngineSettings;

namespace TowerDefense.Source.Attacks.Projectiles
{
    internal class SupperArrow : Projectile
    {
        public sealed override IMove MoveType { get; protected set; }

        public SupperArrow(int collisionDamage, double speed) : base(collisionDamage, speed)
        {
            MoveType = new ArchMove();
        }

        public override object Clone()
        {
            var superArrow = (SupperArrow)MemberwiseClone();
            superArrow.MoveType = (IMove)MoveType.Clone();
            return superArrow;
        }

        public override void Upgrade()
        {
            CollisionDamage = (int)(CollisionDamage * Constants.ProjectileDamageMultiplier.SuperArrow);
        }
    }
}
