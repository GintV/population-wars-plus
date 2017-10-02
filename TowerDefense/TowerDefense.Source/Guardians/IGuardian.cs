using System;

namespace TowerDefense.Source.Guardians
{
    public interface IGuardian
    {
        Guid Id { get; }
        void Attack();
        void ActivateChargeAttack();
        void Promote();
        void Upgrade();
    }
}