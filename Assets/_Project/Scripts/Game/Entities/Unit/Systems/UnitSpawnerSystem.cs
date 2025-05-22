using _Project.Scripts.Game.Entities.Character._Configs;
using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Unit._Configs;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Factories;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Factories.StateMachine;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Entities.Unit.Systems
{
  public class UnitSpawnerSystem : SystemComponent<UnitSpawnerComponent>
  {
    private IStateMachineFactory _stateMachineFactory;
    private IWeaponFactory _weaponFactory;
    private IGameFactory _gameFactory;
    private UnitsConfig _unitsConfig;
    private IStaticDataService _staticData;
    private WeaponsConfig _weaponsConfig;
    private CharacterConfig _characterConfig;

    [Inject]
    private void Construct(IGameFactory gameFactory, IStateMachineFactory stateMachineFactory, UnitsConfig unitsConfig, 
      IWeaponFactory weaponFactory, IStaticDataService staticData, WeaponsConfig weaponsConfig, CharacterConfig characterConfig)
    {
      _characterConfig = characterConfig;
      _weaponsConfig = weaponsConfig;
      _staticData = staticData;
      _weaponFactory = weaponFactory;
      _stateMachineFactory = stateMachineFactory;
      _unitsConfig = unitsConfig;
      _gameFactory = gameFactory;
    }
    
    protected override void OnEnableComponent(UnitSpawnerComponent armament)
    {
      base.OnEnableComponent(armament);
      
      CreateUnit(armament).Forget();
    }
    
    private async UniTaskVoid CreateUnit(UnitSpawnerComponent spawner)
    {
      UnitData unitData = _unitsConfig.Data[spawner.Unit];
      UnitComponent unit = await _gameFactory.CreateUnit(spawner.Unit, spawner.Position, spawner.transform.parent);
      
      WeaponComponent weapon = await _weaponFactory.CreateWeaponComponent(unitData.Weapon, unit.WeaponMediator.Container);
      unit.WeaponMediator.SetWeapon(weapon);
      unit.Animator.SetAnimatorController(AnimatorController());

      unit.SetStats(unitData);

      unit.Health.SetBaseHealth(unitData.Health);
      unit.Health.SetMaxHealth(unitData.Health);
      unit.Health.CurrentHealth.SetValueAndForceNotify(unitData.Health);
      
      unit.StateMachine.CreateStateMachine(_stateMachineFactory.CreateUnitStateMachine(unit));
    }
    
    private RuntimeAnimatorController AnimatorController() =>
      _staticData.UnitAnimatorsPreset().Controllers[_weaponsConfig.Data[_characterConfig.Weapon].WeaponType];

  }
}