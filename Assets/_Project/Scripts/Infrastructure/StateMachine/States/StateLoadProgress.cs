using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.StateMachine.Machine;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  public sealed class StateLoadProgress : IEnterState
  {
    private readonly IGameStateMachine _gameStateMachine;
    private ISceneLoaderService _sceneLoaderService;

    public StateLoadProgress(IGameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
    }

    [Inject]
    private void Construct(ISceneLoaderService sceneLoaderService)
    {
      _sceneLoaderService = sceneLoaderService;
    }

    UniTask IEnterState.Enter()
    {
      _gameStateMachine.Enter<StateLoadTargetScene>();

      return UniTask.CompletedTask;
    }

    UniTask IExitState.Exit()
    {
      return UniTask.CompletedTask;
    }
  }
}