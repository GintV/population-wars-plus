using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Guardians.States
{
    public class ReadyState : State
    {
        private readonly Guardian _guardian;

        public ReadyState(long initialCounter, Guardian guardian) : base(initialCounter)
        {
            _guardian = guardian;
            Counter = 1;
        }

        public override List<Projectile> InitiateAttack(Vector2 target, int targetSpeed) => _guardian.AttackType.Shoot(target, targetSpeed);

        public override void ChangeState(Guardian guardian) => guardian.GuardianState = new LoadingState(InitialCounter);
    }
}
