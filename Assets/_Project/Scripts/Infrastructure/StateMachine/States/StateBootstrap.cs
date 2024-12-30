using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.StateMachine.Machine;
using _Project.Scripts.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  public sealed class StateBootstrap : IEnterState
  {
    private readonly IGameStateMachine _gameStateMachine;
  
    private  ISceneLoaderService _sceneLoaderService;
    private IStaticDataService _staticDataService;

    public StateBootstrap(IGameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
    }

    [Inject]
    private void Construct(ISceneLoaderService sceneLoaderService, IStaticDataService staticDataService)
    {
      _staticDataService = staticDataService;
      _sceneLoaderService = sceneLoaderService;
    }


    UniTask IEnterState.Enter()
    {
      LoadResources();

      EnterStateLoadProgressState();
    
      return UniTask.CompletedTask;
    }

    UniTask IExitState.Exit()
    {
      return UniTask.CompletedTask;
    }

    private void LoadResources() => _staticDataService.Load();
  

    private void EnterStateLoadProgressState() => _gameStateMachine.Enter<StateLoadProgress>();
  }
}