using System;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;

namespace TowerDefense.Source.Guardians
{
    public interface IGuardian
    {
        Guid Id { get; }
        int AttackPower { get; set; }
        IAttack AttackType { get; }
        void Attack();
        void ActivateChargeAttack();
        void Promote();
        void Upgrade();
    }
}