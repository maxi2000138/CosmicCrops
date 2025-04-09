using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Data;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Pool.Item;
using _Project.Scripts.Infrastructure.StaticData;
using CodeBase.Infrastructure.Pool;
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
    
    async UniTask<IProjectile> IWeaponFactory.CreateProjectile(ProjectileType type, Transform spawnPoint, int damage, Vector3 direction)
    {
      // ProjectileData data = _projectilesConfig.Data[type];
      // MonoSpawnableItem prefab = await _assetService.LoadFromAddressable<MonoSpawnableItem>(data.Prefab.Name);
      // CBullet bullet = _objectPoolService.SpawnObject(prefab, spawnPoint.position, spawnPoint.rotation, null).GetComponent<CBullet>();
      // bullet.LifeTime = data.LifeTime;
      // bullet.SetDamage(damage);
      // bullet.SetDirection(direction);
      // bullet.SetCollisionDistance(Mathf.Pow(data.CollisionRadius, 2));
      // return bullet;

      return null;
    }
  }
}