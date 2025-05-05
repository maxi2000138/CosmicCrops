using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Factories;
using VContainer;

namespace _Project.Scripts.Game.Features.Weapon
{
  public class CharacterRangeWeapon : BaseRangeWeapon
  {
    public CharacterRangeWeapon(WeaponComponent weapon, WeaponCharacteristicData weaponCharacteristic) 
      : base(weapon, weaponCharacteristic)
    {
      
    }

    [Inject]
    private void Construct(IWeaponFactory weaponFactory)
    {
      WeaponFactory = weaponFactory;
    }
  }
}