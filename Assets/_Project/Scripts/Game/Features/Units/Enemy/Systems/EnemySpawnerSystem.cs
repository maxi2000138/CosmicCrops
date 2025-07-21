using _Project.Scripts.Game.Features.Units.Character._Configs;
using _Project.Scripts.Game.Features.Units.Enemy._Configs;
using _Project.Scripts.Game.Features.Units.Enemy.Components;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Game.Features.Weapon.Services.Factories;
using _Project.Scripts.Infrastructure.Factories.Game;
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
    private CharacterConfig _characterConfig;

    [Inject]
    private void Construct(IGameFactory gameFactory, IStateMachineFactory stateMachineFactory, EnemiesConfig enemiesConfig, 
      IWeaponFactory weaponFactory, IStaticDataService staticData, WeaponsConfig weaponsConfig, CharacterConfig characterConfig)
    {
      _characterConfig = characterConfig;
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
      
      WeaponComponent weapon = await _weaponFactory.CreateWeaponComponent(1, enemy.WeaponMediator.Container);
      enemy.WeaponMediator.SetWeapon(weapon);
      enemy.Animator.SetAnimatorController(AnimatorController());

      enemy.SetStats(enemyData);

      enemy.Health.SetBaseHealth(enemyData.Health);
      enemy.Health.SetMaxHealth(enemyData.Health);
      enemy.Health.CurrentHealth.SetValueAndForceNotify(enemyData.Health);
      
      enemy.StateMachine.CreateStateMachine(_stateMachineFactory.CreateEnemyStateMachine(enemy));
    }
    
    private RuntimeAnimatorController AnimatorController() =>
      _staticData.UnitAnimatorsPreset().Controllers[_weaponsConfig.Data[1].WeaponType];

  }
}