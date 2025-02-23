using System;
using _Project.Scripts.Game.Entities._Interfaces;

namespace _Project.Scripts.Game.Features.Weapon.Interfaces
{
  public interface IWeapon : IDisposable
  {
    void Attack(ITarget target = null);
    bool CanAttack();
    float AttackDistance();
    float DetectionDistance();
    float AimingSpeed();
    void Execute();
  }
}
