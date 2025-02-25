﻿using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Infrastructure.Logger;

namespace _Project.Scripts.Game.Features.Weapon
{
  public class BaseMeleeWeapon : BaseWeapon
  {
    public BaseMeleeWeapon(WeaponCharacteristicData weaponCharacteristic) : base(weaponCharacteristic)
    {
      
    }
    
    public override void Attack(ITarget target = null)
    {
      base.Attack(target);
      
      if (target != null)
      {
        DebugLogger.Log("Melee attack target: " + target, LogsType.Weapon);
      }
    }
  }

}