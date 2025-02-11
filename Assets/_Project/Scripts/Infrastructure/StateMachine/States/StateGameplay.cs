using _Project.Scripts.Game.Level.Model;
using _Project.Scripts.Game.Units.Character.StateMachine.States;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.UI.Screens;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateGameplay : IEnterState, IExitState
  {
    private IUIFactory _uiFactory;
    private IJoystickService _joystickService;
    private LevelModel _levelModel;

    [Inject]
    private void Construct(IUIFactory uiFactory, IJoystickService joystickService, LevelModel levelModel)
    {
      _levelModel = levelModel;
      _joystickService = joystickService;
      _uiFactory = uiFactory;

    }

    public async UniTask Enter(IGameStateMachine gameStateMachine)
    {
      await _uiFactory.CreateScreen(ScreenType.Game);
      _joystickService.Enable(true);
      
      ActivateUnitStateMachine();
    }
    
    public UniTask Exit(IGameStateMachine gameStateMachine)
    {
      _joystickService.Enable(false);
      
      return UniTask.CompletedTask;
    }
    
    private void ActivateUnitStateMachine()
    {
      _levelModel.Character.StateMachine.StateMachine.Enter<CharacterStateIdle>();
    }

  }
}