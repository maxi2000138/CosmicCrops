using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Componets;

namespace _Project.Scripts.Game.Features.Weapon
{
  public class CharacterMeleeWeapon : BaseMeleeWeapon
  {
    public CharacterMeleeWeapon(WeaponComponent weapon, WeaponCharacteristicData weaponCharacteristic, IAbilityApplier abilityApplier) 
      : base(weapon, weaponCharacteristic, abilityApplier)
    {
      
    }
  }

}