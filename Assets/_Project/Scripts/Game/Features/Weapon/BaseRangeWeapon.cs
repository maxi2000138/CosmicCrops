using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Services.Factories;
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

    public override void Attack(IUnit unit = null)
    {
      base.Attack(unit);
            
      Shoot(unit);
    }

    private void Shoot(IUnit unit = null)
    {
      CreateBullet(unit).Forget();
    }

    protected async virtual UniTaskVoid CreateBullet(IUnit unit = null) { }
  }
}