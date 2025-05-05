using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Pool;
using _Project.Scripts.Infrastructure.Pool.Item;
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
    private readonly ProjectilesConfig _projectilesConfig;
    private readonly IAssetService _assetService;
    private readonly IObjectPoolService _objectPoolService;
    private readonly WeaponsConfig _weaponsConfig;

    public WeaponFactory(WeaponsConfig weaponsConfig, IObjectResolver objectResolver, IAbilityApplier abilityApplier, ProjectilesConfig projectilesConfig, 
      IAssetService assetService, IObjectPoolService objectPoolService)
    {
      _weaponsConfig = weaponsConfig;
      _objectResolver = objectResolver;
      _abilityApplier = abilityApplier;
      _projectilesConfig = projectilesConfig;
      _assetService = assetService;
      _objectPoolService = objectPoolService;
    }
  
    async UniTask<WeaponComponent> IWeaponFactory.CreateCharacterWeapon(WeaponType type, Transform parent)
    {
      WeaponCharacteristicData data = _weaponsConfig.Data[type];
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.Prefab.Name);
      WeaponComponent weapon = Object.Instantiate(prefab, parent.position, parent.rotation).GetComponent<WeaponComponent>();
      weapon.transform.SetParent(parent, true);

      weapon.SetWeapon(CreateSpecificCharacterWeapon(weapon, type, data));
      return weapon;
    }
  
    private IWeapon CreateSpecificCharacterWeapon(WeaponComponent weapon, WeaponType type, WeaponCharacteristicData weaponCharacteristic)
    {
      BaseWeapon currentWeapon = type == WeaponType.Knife
        ? new CharacterMeleeWeapon(weapon, weaponCharacteristic, _abilityApplier)
        : new CharacterRangeWeapon(weapon, weaponCharacteristic);
      
      _objectResolver.Inject(currentWeapon);
      currentWeapon.Initialize();
      return currentWeapon;
    }
    
    async UniTask<IProjectile> IWeaponFactory.CreateProjectile(ProjectileType type, Transform spawnPoint, string ability, Vector3 direction)
    {
      ProjectileData data = _projectilesConfig.Data[type];
      MonoSpawnableItem prefab = (await _assetService.LoadFromAddressable<GameObject>(data.Prefab.Name)).GetComponent<MonoSpawnableItem>();
      BulletComponent bullet = _objectPoolService.SpawnObject(prefab, spawnPoint.position, spawnPoint.rotation, null).GetComponent<BulletComponent>();
      bullet.LifeTime = data.LifeTime;
      bullet.SetAbility(ability);
      bullet.SetDirection(direction);
      bullet.SetCollisionDistance(Mathf.Pow(data.CollisionRadius, 2));
      bullet.SetCollisionMask(data.CollisionMask);
      return bullet;
    }
  }
}