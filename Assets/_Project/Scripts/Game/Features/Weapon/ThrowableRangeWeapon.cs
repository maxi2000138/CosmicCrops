using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Factories;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.Features.Weapon
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

    protected override async UniTaskVoid CreateBullet(ITarget target = null) 
    {
      await WeaponFactory.CreateThrowable(WeaponCharacteristic.Armament, Weapon.SpawnPoint, WeaponCharacteristic.Ability, WeaponCharacteristic.ForceBullet, target);
    }
  }
}