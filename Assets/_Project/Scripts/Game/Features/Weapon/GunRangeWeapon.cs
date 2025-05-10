using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Features.Weapon
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

    protected override async UniTaskVoid CreateBullet(ITarget target = null) 
    {
      Vector3 normalized = Weapon.SpawnPoint.forward.normalized;
      Vector3 direction = new Vector3(normalized.x, 0f, normalized.z);
        
      await WeaponFactory.CreateBullet(WeaponCharacteristic.Armament, Weapon.SpawnPoint, WeaponCharacteristic.Ability, WeaponCharacteristic.ForceBullet, direction);
    }
  }
}