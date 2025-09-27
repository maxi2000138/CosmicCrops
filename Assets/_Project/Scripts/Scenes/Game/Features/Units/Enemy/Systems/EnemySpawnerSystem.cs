using _Project.Scripts.Game.Features.Inventory;
using _Project.Scripts.Game.Features.Units.Enemy._Configs;
using _Project.Scripts.Game.Features.Units.Enemy.Components;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Game.Features.Weapon.Services.Factories;
using _Project.Scripts.Game.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Factories.StateMachine;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Features.Units.Enemy.Systems
{
  public class EnemySpawnerSystem : SystemComponent<EnemySpawnerComponent>
  {
    private IStateMachineFactory _stateMachineFactory;
    private IWeaponFactory _weaponFactory;
    private IGameFactory _gameFactory;
    private EnemiesConfig _enemiesConfig;
    private IStaticDataService _staticData;
    private WeaponsConfig _weaponsConfig;
    private InventoryModel _inventoryModel;

    [Inject]
    private void Construct(IGameFactory gameFactory, IStateMachineFactory stateMachineFactory, EnemiesConfig enemiesConfig, 
      IWeaponFactory weaponFactory, IStaticDataService staticData, WeaponsConfig weaponsConfig, InventoryModel inventoryModel)
    {
      _inventoryModel = inventoryModel;
      _weaponsConfig = weaponsConfig;
      _staticData = staticData;
      _weaponFactory = weaponFactory;
      _stateMachineFactory = stateMachineFactory;
      _enemiesConfig = enemiesConfig;
      _gameFactory = gameFactory;
    }
    
    protected override void OnEnableComponent(EnemySpawnerComponent armament)
    {
      base.OnEnableComponent(armament);
      
      CreateEnemy(armament).Forget();
    }
    
    private async UniTaskVoid CreateEnemy(EnemySpawnerComponent spawner)
    {
      EnemyData enemyData = _enemiesConfig.Data[spawner.Enemy];
      EnemyComponent enemy = await _gameFactory.CreateUnit(spawner.Enemy, spawner.Position, spawner.transform.parent);
      
    // TODO: set from config
      WeaponComponent weapon = await _weaponFactory.CreateWeaponComponent(enemyData.Weapon, enemy.WeaponMediator.Container);
      enemy.WeaponMediator.SetWeapon(weapon);
      enemy.Animator.SetAnimatorController(AnimatorController(enemyData));

      enemy.SetStats(enemyData);

      enemy.Health.SetBaseHealth(enemyData.Health);
      enemy.Health.SetMaxHealth(enemyData.Health);
      enemy.Health.CurrentHealth.SetValueAndForceNotify(enemyData.Health);
      
      enemy.StateMachine.CreateStateMachine(_stateMachineFactory.CreateEnemyStateMachine(enemy));
      
      SetSkin(enemy);
    }
    
    private void SetSkin(EnemyComponent enemy)
    {
      int skinIndex = enemy.BodyMediator.Skins.GetRandomIndex();

      for (int i = 0; i < enemy.BodyMediator.Skins.Length; i++)
      {
        enemy.BodyMediator.Skins[i].Visual.SetActive(i == skinIndex);
      }
    }
    
    // TODO: set from config
    private RuntimeAnimatorController AnimatorController(EnemyData enemyData) =>
      _staticData.UnitAnimatorsPreset().Controllers[_weaponsConfig.Data[enemyData.Weapon].WeaponType];

  }
}