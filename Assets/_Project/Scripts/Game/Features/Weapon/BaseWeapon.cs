using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon
{
  public class BaseWeapon : IWeapon
  {
    protected readonly WeaponCharacteristicData WeaponCharacteristic;
    
    private int _clipCount;
    private bool _canAttack;
    private float _rechargeDelay;
    private float _fireIntervalDelay;

    protected float AttackDistance;

    public BaseWeapon(WeaponCharacteristicData weaponCharacteristic)
    {
      WeaponCharacteristic = weaponCharacteristic;
    }
    
    public virtual void Dispose()
    {
      
    }
    
    public void Initialize()
    {
      AttackDistance = Mathf.Pow(WeaponCharacteristic.AttackDistance, 2);
            
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
    float IWeapon.AttackDistance() => AttackDistance;
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