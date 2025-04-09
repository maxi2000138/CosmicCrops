using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Data;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon
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
      _attackDistance = Mathf.Pow(WeaponCharacteristic.AttackDistance, 2);
      _attackInterval = WeaponCharacteristic.FireInterval;
            
      ReadyAttack();
      ReloadClip();
    }

    public virtual void Attack(ITarget target = null)
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