using System;
using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using _Project.Scripts.Game.Features.Weapon.Variations;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Pool;
using _Project.Scripts.Infrastructure.Pool.Item;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Game.Features.Weapon.Services.Factories
{
  public class WeaponFactory : IWeaponFactory
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IObjectResolver _objectResolver;
    private readonly IAbilityApplier _abilityApplier;
    private readonly ProjectilesConfig _projectilesConfig;
    private readonly IAssetProvider _assetProvider;
    private readonly IObjectPoolService _objectPoolService;
    private readonly WeaponsConfig _weaponsConfig;

    public WeaponFactory(WeaponsConfig weaponsConfig, IObjectResolver objectResolver, IAbilityApplier abilityApplier, ProjectilesConfig projectilesConfig, 
      IAssetProvider assetProvider, IObjectPoolService objectPoolService)
    {
      _weaponsConfig = weaponsConfig;
      _objectResolver = objectResolver;
      _abilityApplier = abilityApplier;
      _projectilesConfig = projectilesConfig;
      _assetProvider = assetProvider;
      _objectPoolService = objectPoolService;
    }
  
    async UniTask<WeaponComponent> IWeaponFactory.CreateWeaponComponent(int weaponId, Transform parent)
    {
      WeaponCharacteristicData data = _weaponsConfig.Data[weaponId];
      GameObject prefab = await _assetProvider.LoadFromAddressable<GameObject>(data.WeaponPrefab.Name);
      WeaponComponent weapon = Object.Instantiate(prefab, parent.position, parent.rotation).GetComponent<WeaponComponent>();
      weapon.transform.SetParent(parent, true);

      weapon.SetWeapon(CreateSpecificWeapon(weapon, data));
      return weapon;
    }
  
    private IWeapon CreateSpecificWeapon(WeaponComponent weapon, WeaponCharacteristicData weaponCharacteristic)
    {
      BaseWeapon currentWeapon;
      switch(weaponCharacteristic.WeaponType)
      {
        case WeaponType.Melee:
          currentWeapon = new MeleeWeapon(weapon, weaponCharacteristic, _abilityApplier);
          break;
        case WeaponType.Ranged:
          currentWeapon = new GunRangeWeapon(weapon, weaponCharacteristic);
          break;
        case WeaponType.Throwable:
          currentWeapon = new ThrowableRangeWeapon(weapon, weaponCharacteristic);
          break;
        
        default:
          throw new ArgumentOutOfRangeException($"Weapon type " + weaponCharacteristic.WeaponType + " not found!");
      }
      
      _objectResolver.Inject(currentWeapon);
      currentWeapon.Initialize();
      return currentWeapon;
    }
    
    async UniTask<IProjectile> IWeaponFactory.CreateBullet(string armament, Transform spawnPoint, string ability, float speed, Vector3 direction)
    {
      ProjectileData data = _projectilesConfig.Data[armament];
      MonoSpawnableItem prefab = (await _assetProvider.LoadFromAddressable<GameObject>(data.Prefab.Name)).GetComponent<MonoSpawnableItem>();
      ArmamentComponent bullet = _objectPoolService.SpawnObject(prefab, spawnPoint.position, spawnPoint.rotation, null).GetComponent<ArmamentComponent>();
      bullet.LifeTime = data.LifeTime;
      bullet.SetAbility(ability);
      bullet.SetSpeed(speed);
      bullet.DirectionComponent.SetTargetDirection(direction);
      bullet.SetCollisionSqrDistance(Mathf.Pow(data.CollisionRadius, 2));
      bullet.SetCollisionMask(data.CollisionMask);
      return bullet;
    }
    
    async UniTask<IProjectile> IWeaponFactory.CreateThrowable(string armament, Transform spawnPoint, string ability, float speed, IUnit unit)
    {
      ProjectileData data = _projectilesConfig.Data[armament];
      MonoSpawnableItem prefab = (await _assetProvider.LoadFromAddressable<GameObject>(data.Prefab.Name)).GetComponent<MonoSpawnableItem>();
      ArmamentComponent potion = _objectPoolService.SpawnObject(prefab, spawnPoint.position, spawnPoint.rotation, null).GetComponent<ArmamentComponent>();
      potion.LifeTime = data.LifeTime;
      potion.SetAbility(ability);
      potion.SetSpeed(speed);
      potion.HomingComponent.SetTarget(unit);
      potion.ThrowableComponent.SetInitialSqrDistance(spawnPoint.position.HorizontalProjectedSqrDistance(unit.Position));
      potion.SetCollisionSqrDistance(Mathf.Pow(data.CollisionRadius, 2));
      potion.SetCollisionMask(data.CollisionMask);
      return potion;
    }
  }
}