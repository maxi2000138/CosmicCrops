using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon
{
  public class BaseRangeWeapon : BaseWeapon
  {
    protected IWeaponFactory WeaponFactory;
        
    protected BaseRangeWeapon(WeaponComponent weapon, WeaponCharacteristicData weaponCharacteristic) 
      : base(weapon, weaponCharacteristic)
    {
      
    }

    public override void Attack(ITarget target = null)
    {
      base.Attack(target);
            
      Shoot(target);
    }

    private void Shoot(ITarget target = null)
    {
      CreateBullet(target).Forget();
    }

    protected async virtual UniTaskVoid CreateBullet(ITarget target = null) { }
  }
}