using _Project.Scripts.Game.Weapon.Componets;
using _Project.Scripts.Game.Weapon.Data;
using _Project.Scripts.Game.Weapon.Interfaces;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
namespace _Project.Scripts.Game.Weapon.Factories
{
  public class WeaponFactory : IWeaponFactory
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IObjectResolver _objectResolver;
    public WeaponFactory(IStaticDataService staticDataService, IObjectResolver objectResolver)
    {
      _staticDataService = staticDataService;
      _objectResolver = objectResolver;
    }
  
    async UniTask<WeaponComponent> IWeaponFactory.CreateCharacterWeapon(WeaponComponent weapon, WeaponType type, Transform parent)
    {
      WeaponCharacteristicData data = _staticDataService.WeaponCharacteristicConfig().Data[type];
      weapon.SetWeapon(CreateSpecificCharacterWeapon(type, data.WeaponCharacteristic));
      return weapon;
    }
  
    private IWeapon CreateSpecificCharacterWeapon(WeaponType type, WeaponCharacteristic weaponCharacteristic)
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