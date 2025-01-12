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
  public sealed class StateGameplayLoop : IEnterState, IExitState
  {
    private IUIFactory _uiFactory;
    private IJoystickService _joystickService;

    [Inject]
    private void Construct(IUIFactory uiFactory, IJoystickService joystickService)
    {
      _joystickService = joystickService;
      _uiFactory = uiFactory;

    }

    public UniTask Enter(IGameStateMachine gameStateMachine)
    {
      _uiFactory.CreateScreen(ScreenType.Game);
      _joystickService.Enable(true);

      return UniTask.CompletedTask;
    }
    public UniTask Exit(IGameStateMachine gameStateMachine)
    {
      _joystickService.Enable(false);
      
      return UniTask.CompletedTask;
    }
  }
}