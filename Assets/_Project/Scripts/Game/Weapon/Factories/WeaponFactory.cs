using _Project.Scripts.Game.Weapon._Configs;
using _Project.Scripts.Game.Weapon.Componets;
using _Project.Scripts.Game.Weapon.Data;
using _Project.Scripts.Game.Weapon.Interfaces;
using _Project.Scripts.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
namespace _Project.Scripts.Game.Weapon.Factories
{
  public class WeaponFactory : IWeaponFactory
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IObjectResolver _objectResolver;
    private readonly WeaponsConfig _weaponsConfig;
    public WeaponFactory(WeaponsConfig weaponsConfig, IObjectResolver objectResolver)
    {
      _weaponsConfig = weaponsConfig;
      _objectResolver = objectResolver;
    }
  
    async UniTask<WeaponComponent> IWeaponFactory.CreateCharacterWeapon(WeaponComponent weapon, WeaponType type, Transform parent)
    {
      weapon.SetWeapon(CreateSpecificCharacterWeapon(type, _weaponsConfig.Data[type]));
      return weapon;
    }
  
    private IWeapon CreateSpecificCharacterWeapon(WeaponType type, WeaponCharacteristicData weaponCharacteristic)
    {
      BaseWeapon currentWeapon = type == WeaponType.Knife
        ? new CharacterMeleeWeapon(weaponCharacteristic)
        : null;
      
      _objectResolver.Inject(currentWeapon);
      currentWeapon.Initialize();
      return currentWeapon;
    }
  }
}