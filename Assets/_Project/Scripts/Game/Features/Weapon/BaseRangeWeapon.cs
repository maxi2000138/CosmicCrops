using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs;
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
            
      Shoot();
    }

    private void Shoot()
    {
      CreateBullet().Forget();
    }

    private async UniTaskVoid CreateBullet()
    {
        Vector3 normalized = Weapon.SpawnPoint.forward.normalized;
        Vector3 direction = new Vector3(normalized.x, 0f, normalized.z) * WeaponCharacteristic.ForceBullet;
        
        await WeaponFactory.CreateProjectile(Weapon.ProjectileType, Weapon.SpawnPoint, WeaponCharacteristic.Ability, direction);
    }

  }
}