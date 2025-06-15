using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Components;

namespace _Project.Scripts.Game.Features.Weapon.Variations
{
  public class MeleeWeapon : BaseMeleeWeapon
  {
    public MeleeWeapon(WeaponComponent weapon, WeaponCharacteristicData weaponCharacteristic, IAbilityApplier abilityApplier) 
      : base(weapon, weaponCharacteristic, abilityApplier)
    {
      
    }
  }

}