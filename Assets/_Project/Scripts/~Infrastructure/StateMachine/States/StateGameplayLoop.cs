using _Project.Scripts._Infrastructure.Factories.UI;
using _Project.Scripts._Infrastructure.StateMachine.Machine;
using _Project.Scripts.UI.Screens;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer;

namespace _Project.Scripts._Infrastructure.StateMachine.States
{
  
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateGameplayLoop : IEnterState
  {
    private IUIFactory _uiFactory;
    
    [Inject]
    private void Construct(IUIFactory uiFactory)
    {
      _uiFactory = uiFactory;

    }

    public UniTask Enter(IGameStateMachine gameStateMachine)
    {
      _uiFactory.CreateScreen(ScreenType.Game);

      return UniTask.CompletedTask;
    }
  }
}