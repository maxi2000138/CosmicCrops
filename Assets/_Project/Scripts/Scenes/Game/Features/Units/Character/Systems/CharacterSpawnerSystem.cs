using _Project.Scripts.Game.Features.Collector.Factory;
using _Project.Scripts.Game.Features.Inventory;
using _Project.Scripts.Game.Features.Units.Character.Components;
using _Project.Scripts.Game.Features.Units.Character.StateMachine.States;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Game.Features.Weapon.Services.Factories;
using _Project.Scripts.Game.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Factories.StateMachine;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Scenes.Game.Common._Config;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Features.Units.Character.Systems
{
  public class CharacterSpawnerSystem : SystemComponent<CharacterSpawnerComponent>
  {
    private IGameFactory _gameFactory;
    private ICameraService _cameraService;
    private IStateMachineFactory _stateMachineFactory;
    private InventoryModel _inventoryModel;
    private ICollectorFactory _collectorFactory;
    private IWeaponFactory _weaponFactory;
    private IStaticDataService _staticData;
    private WeaponsConfig _weaponsConfig;
    private CommonConstantsConfig _commonConstantsConfig;

    [Inject]
    private void Construct(IGameFactory gameFactory, ICollectorFactory collectorFactory, ICameraService cameraService, 
      IStateMachineFactory stateMachineFactory, InventoryModel inventoryModel, IWeaponFactory weaponFactory,
      IStaticDataService staticData, WeaponsConfig weaponsConfig, CommonConstantsConfig commonConstantsConfig)
    {
      _commonConstantsConfig = commonConstantsConfig;
      _weaponsConfig = weaponsConfig;
      _staticData = staticData;
      _weaponFactory = weaponFactory;
      _collectorFactory = collectorFactory;
      _inventoryModel = inventoryModel;
      _stateMachineFactory = stateMachineFactory;
      _cameraService = cameraService;
      _gameFactory = gameFactory;
    }
    
    protected override void OnEnableComponent(CharacterSpawnerComponent armament)
    {
      base.OnEnableComponent(armament);
      
      CreateCharacter(armament).Forget();
    }
    
    private async UniTaskVoid CreateCharacter(CharacterSpawnerComponent spawner)
    {
      var character = await _gameFactory.CreateCharacter(spawner.Position, spawner.transform.parent);

      WeaponComponent weapon = await _weaponFactory.CreateWeaponComponent(_inventoryModel.SelectedWeapon.CurrentValue, character.WeaponMediator.Container);
      character.WeaponMediator.SetWeapon(weapon);
      character.UnitAnimator.SetAnimatorController(AnimatorController());
      
      character.StateMachine.CreateStateMachine(_stateMachineFactory.CreateCharacterStateMachine(character));
      character.StateMachine.UnitStateMachine.Enter<CharacterStateIdle>();

      character.CharacterController.SetSpeed(character.CharacterController.BaseSpeed);
      
      character.Collector.SetCollector(_collectorFactory.CreateDefault());

      character.Health.SetBaseHealth(_commonConstantsConfig.CharacterHealth);
      character.Health.SetMaxHealth(_commonConstantsConfig.CharacterHealth);
      character.Health.CurrentHealth.SetValueAndForceNotify(_commonConstantsConfig.CharacterHealth);
      
        
      character.SetHeight(_commonConstantsConfig.CharacterHeight);

      SetSkin(character);
      SetCameraTarget(character);
      SetRadarRadius(character);
    }
    
    private void SetSkin(CharacterComponent character)
    {
      for (int i = 0; i < character.BodyMediator.Skins.Length; i++)
      {
        character.BodyMediator.Skins[i].Visual
          .SetActive(character.BodyMediator.Skins[i].SkinId == _inventoryModel.SelectedSkin.Value);
      }
    }

    private void SetCameraTarget(CharacterComponent character)
    {
      _cameraService.SetTarget(character.transform);
    }
    
    private void SetRadarRadius(CharacterComponent character)
    {
      float distance = character.WeaponMediator.CurrentWeapon.Weapon.AttackDistance();
      character.Radar.SetRadius(distance);
    }
    
    private RuntimeAnimatorController AnimatorController() =>
      _staticData.UnitAnimatorsPreset().Controllers[_weaponsConfig.Data[_inventoryModel.SelectedWeapon.CurrentValue].WeaponType];
  }
}