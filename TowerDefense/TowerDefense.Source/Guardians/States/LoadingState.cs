using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Guardians.States
{
    public class LoadingState : State
    {
        public LoadingState(int initialCounter) : base(initialCounter) { }

        public override List<Projectile> InitiateAttack(Vector2 target, int targetSpeed) => new List<Projectile>();

        public override void ChangeState(Guardian guardian) => guardian.GuardianState = new ReadyState(InitialCounter, guardian);
    }
}
