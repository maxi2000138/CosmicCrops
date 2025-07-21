using _Project.Scripts.Infrastructure.Progress;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  public sealed class StateLoadProgress : IEnterState
  {
    private readonly IProgressService _progressService;
    public StateLoadProgress(IProgressService progressService)
    {
      _progressService = progressService;
    }
    
    UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      _progressService.Init();

      gameStateMachine.Enter<StateInitGameServices>().Forget();
      return UniTask.CompletedTask;
    }
  }
}