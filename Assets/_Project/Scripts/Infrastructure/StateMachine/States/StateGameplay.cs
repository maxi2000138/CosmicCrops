using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Character.StateMachine.States;
using _Project.Scripts.Game.Entities.Unit.StateMachine.States;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.UI.Screens;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.StateMachine.Data;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using ObservableCollections;
using R3;
using VContainer;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateGameplay : IEnterState, IExitState
  {
    private readonly IJoystickService _joystickService;
    private readonly LevelModel _levelModel;
    private readonly IUIFactory _uiFactory;
    private IGameStateMachine _gameStateMachine;
    
    private readonly CompositeDisposable _transitionDisposable = new();

    public StateGameplay(IUIFactory uiFactory, IJoystickService joystickService, LevelModel levelModel)
    {
      _levelModel = levelModel;
      _joystickService = joystickService;
      _uiFactory = uiFactory;

    }

    public async UniTask Enter(IGameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
      await _uiFactory.CreateScreen(ScreenType.Game);
      _joystickService.Enable(true);
      
      ActivateUnitStateMachine();
      
      SubscribeOnWin();
    }
    
    public UniTask Exit(IGameStateMachine gameStateMachine)
    {
      _joystickService.Enable(false);
      _transitionDisposable.Clear();
      
      return UniTask.CompletedTask;
    }

    private void ActivateUnitStateMachine()
    {
      _levelModel.Character.StateMachine.StateMachine.Enter<CharacterStateIdle>();
      _levelModel.Enemies.Foreach(SetEnemyStateIdle);
    }
    
    private void SubscribeOnWin()
    {
      _levelModel.Loot
        .ObserveRemove()
        .First(_ => AllLootCollected())
        .Subscribe(_ => Win())
        .AddTo(_transitionDisposable);
    }
    
    private void Win()
    {
      _gameStateMachine.Enter<StateGameResult, GameResult>(GameResult.Win);
      _levelModel.Character.StateMachine.StateMachine.Enter<CharacterStateNone>();
    }
    
    private bool AllLootCollected() => _levelModel.Loot.Count == 0;
    private void SetEnemyStateIdle(IEnemy enemy) => enemy.StateMachine.StateMachine.Enter<UnitStateIdle>();
  }
}