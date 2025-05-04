using _Project.Scripts.Game.Entities.Character._Configs;
using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Character.StateMachine.States;
using _Project.Scripts.Game.Features.Collector.Factory;
using _Project.Scripts.Game.Features.Inventory;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Data;
using _Project.Scripts.Game.Features.Weapon.Factories;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Factories.StateMachine;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.Entities.Character.Systems
{
  public class CharacterSpawnerSystem : SystemComponent<CharacterSpawnerComponent>
  {
    private IGameFactory _gameFactory;
    private ICameraService _cameraService;
    private IStateMachineFactory _stateMachineFactory;
    private InventoryModel _inventoryModel;
    private ICollectorFactory _collectorFactory;
    private IWeaponFactory _weaponFactory;
    private CharacterConfig _characterConfig;

    [Inject]
    private void Construct(IGameFactory gameFactory, ICollectorFactory collectorFactory, ICameraService cameraService, 
      IStateMachineFactory stateMachineFactory, InventoryModel inventoryModel, IWeaponFactory weaponFactory, CharacterConfig characterConfig)
    {
      _characterConfig = characterConfig;
      _weaponFactory = weaponFactory;
      _collectorFactory = collectorFactory;
      _inventoryModel = inventoryModel;
      _stateMachineFactory = stateMachineFactory;
      _cameraService = cameraService;
      _gameFactory = gameFactory;
    }
    
    protected override void OnEnableComponent(CharacterSpawnerComponent component)
    {
      base.OnEnableComponent(component);
      
      CreateCharacter(component).Forget();
    }
    
    private async UniTaskVoid CreateCharacter(CharacterSpawnerComponent spawner)
    {
      var character = await _gameFactory.CreateCharacter(spawner.Position, spawner.transform.parent);
      WeaponComponent weapon = await _weaponFactory.CreateCharacterWeapon(WeaponType.Rifle, character.WeaponMediator.Container);
      character.WeaponMediator.SetWeapon(weapon);
      
      character.StateMachine.CreateStateMachine(_stateMachineFactory.CreateCharacterStateMachine(character));
      character.StateMachine.StateMachine.Enter<CharacterStateIdle>();

      character.CharacterController.SetSpeed(character.CharacterController.BaseSpeed);
      
      character.Collector.SetCollector(_collectorFactory.CreateDefault());

      character.Health.SetBaseHealth(_characterConfig.Health);
      character.Health.SetMaxHealth(_characterConfig.Health);
      character.Health.CurrentHealth.SetValueAndForceNotify(_characterConfig.Health);
      
      character.SetHeight(_characterConfig.Height);

      SetCameraTarget(character);
    }
    
    private void SetCameraTarget(CharacterComponent character)
    {
      _cameraService.SetTarget(character.transform);
    }
  }
}