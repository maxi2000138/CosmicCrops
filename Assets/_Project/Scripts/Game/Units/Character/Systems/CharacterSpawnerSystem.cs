using _Project.Scripts.Game.Collector.Collectors;
using _Project.Scripts.Game.Collector.Factory;
using _Project.Scripts.Game.Inventory;
using _Project.Scripts.Game.Units.Character.Components;
using _Project.Scripts.Game.Units.Character.StateMachine.States;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Factories.StateMachine;
using _Project.Scripts.Infrastructure.Systems;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.Units.Character.Systems
{
  public class CharacterSpawnerSystem : SystemComponent<CharacterSpawnerComponent>
  {
    private IGameFactory _gameFactory;
    private ICameraService _cameraService;
    private IStateMachineFactory _stateMachineFactory;
    private InventoryModel _inventoryModel;
    private ICollectorFactory _collectorFactory;

    [Inject]
    private void Construct(IGameFactory gameFactory, ICollectorFactory collectorFactory, ICameraService cameraService, IStateMachineFactory stateMachineFactory, InventoryModel inventoryModel)
    {
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
    
    private async UniTaskVoid CreateCharacter(CharacterSpawnerComponent component)
    {
      var character = await _gameFactory.CreateCharacter(component.Position, component.transform.parent);
      character.StateMachine.CreateStateMachine(_stateMachineFactory.CreateCharacterStateMachine(character));
      character.StateMachine.StateMachine.Enter<CharacterStateIdle>();

      character.CharacterController.SetSpeed(character.CharacterController.BaseSpeed);
      
      character.Collector.SetCollector(_collectorFactory.CreateDefault());

      SetCameraTarget(character);
    }
    
    private void SetCameraTarget(CharacterComponent character)
    {
      _cameraService.SetTarget(character.transform);
    }
  }
}