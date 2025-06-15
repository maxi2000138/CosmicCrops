using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Game.Features.Weapon.Services.Factories;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.Features.Weapon.Variations
{
  public class ThrowableRangeWeapon : BaseRangeWeapon
  {
    public ThrowableRangeWeapon(WeaponComponent weapon, WeaponCharacteristicData weaponCharacteristic) 
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
      await WeaponFactory.CreateThrowable(WeaponCharacteristic.Armament, Weapon.SpawnPoint, WeaponCharacteristic.Ability, WeaponCharacteristic.ForceBullet, unit);
    }
  }
}