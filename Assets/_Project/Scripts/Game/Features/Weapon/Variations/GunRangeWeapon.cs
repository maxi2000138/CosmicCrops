using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Game.Features.Weapon.Services.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Features.Weapon.Variations
{
  public class GunRangeWeapon : BaseRangeWeapon
  {
    public GunRangeWeapon(WeaponComponent weapon, WeaponCharacteristicData weaponCharacteristic) 
      : base(weapon, weaponCharacteristic)
    {
      
    }

    [Inject]
    private void Construct(IWeaponFactory weaponFactory)
    {
      WeaponFactory = weaponFactory;
    }

    protected override async UniTaskVoid CreateBullet(IUnit unit = null) 
    {
      Vector3 normalized = Weapon.SpawnPoint.forward.normalized;
      Vector3 direction = new Vector3(normalized.x, 0f, normalized.z);
        
      await WeaponFactory.CreateBullet(WeaponCharacteristic.Armament, Weapon.SpawnPoint, WeaponCharacteristic.Ability, WeaponCharacteristic.ForceBullet, direction);
    }
  }
}