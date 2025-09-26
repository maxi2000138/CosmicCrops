using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Variations
{
  public class BaseWeapon : IWeapon
  {
    protected readonly WeaponComponent Weapon;
    protected readonly WeaponCharacteristicData WeaponCharacteristic;
    
    private int _clipCount;
    private bool _canAttack;
    private float _rechargeDelay;
    private float _fireIntervalDelay;

    private float _attackDistance;
    private float _attackInterval;

    public BaseWeapon(WeaponComponent weapon, WeaponCharacteristicData weaponCharacteristic)
    {
      WeaponCharacteristic = weaponCharacteristic;
      Weapon = weapon;
    }
    
    public virtual void Dispose()
    {
      
    }
    
    public virtual void Initialize()
    {
      _attackDistance = WeaponCharacteristic.AttackDistance;
      _attackInterval = WeaponCharacteristic.FireInterval;
            
      ReadyAttack();
      ReloadClip();
    }

    public virtual void Attack(IUnit unit = null)
    {
      NotReadyAttack();
      ReduceClip();
      UpdateFireInterval();
            
      if (ClipIsEmpty())
      {
        UpdateRechargeTime();
      }
    }
    
    bool IWeapon.CanAttack() => _clipCount > 0 && _canAttack; 
    float IWeapon.AttackDistance() => _attackDistance;
    float IWeapon.AttackInterval() => _attackInterval;
    float IWeapon.DetectionDistance() => WeaponCharacteristic.DetectionDistance;
    float IWeapon.AimingSpeed() => WeaponCharacteristic.Aiming;
    string IWeapon.Ability() => WeaponCharacteristic.Ability;
    
    void IWeapon.Execute()
    {
      if (ClipIsEmpty())
      {
        _rechargeDelay -= Time.deltaTime;

        if (_rechargeDelay < 0f)
        {
          ReloadClip();
        }
      }

      if (_canAttack == false)
      {
        _fireIntervalDelay -= Time.deltaTime;

        if (_fireIntervalDelay < 0f)
        {
          ReadyAttack();
        }
      }
    }
    
    protected virtual void ReloadClip() => _clipCount = WeaponCharacteristic.ClipCount;
    protected virtual void ReduceClip() => _clipCount--;

    
    private void ReadyAttack() => _canAttack = true;
    private void NotReadyAttack() => _canAttack = false;
    private bool ClipIsEmpty() => _clipCount <= 0;
    private void UpdateRechargeTime() => _rechargeDelay = WeaponCharacteristic.RechargeTime;
    private void UpdateFireInterval() => _fireIntervalDelay = WeaponCharacteristic.FireInterval;
  }
}