using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Data;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using _Project.Scripts.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Features.Weapon.Factories
{
  public class WeaponFactory : IWeaponFactory
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IObjectResolver _objectResolver;
    private readonly IAbilityApplier _abilityApplier;
    private readonly WeaponsConfig _weaponsConfig;
    public WeaponFactory(WeaponsConfig weaponsConfig, IObjectResolver objectResolver, IAbilityApplier abilityApplier)
    {
      _weaponsConfig = weaponsConfig;
      _objectResolver = objectResolver;
      _abilityApplier = abilityApplier;
    }
  
    async UniTask<WeaponComponent> IWeaponFactory.CreateCharacterWeapon(WeaponComponent weapon, WeaponType type, Transform parent)
    {
      weapon.SetWeapon(CreateSpecificCharacterWeapon(weapon, type, _weaponsConfig.Data[type]));
      return weapon;
    }
  
    private IWeapon CreateSpecificCharacterWeapon(WeaponComponent weapon, WeaponType type, WeaponCharacteristicData weaponCharacteristic)
    {
      //TODO: setup correct weapon choose without nullRefs
        
      BaseWeapon currentWeapon = type == WeaponType.Knife
        ? new CharacterMeleeWeapon(weapon, weaponCharacteristic, _abilityApplier)
        : null;
      
      _objectResolver.Inject(currentWeapon);
      currentWeapon.Initialize();
      return currentWeapon;
    }
  }
}