using _Project.Scripts._Infrastructure.SceneLoader;
using _Project.Scripts._Infrastructure.StateMachine.Machine;
using CodeBase.Infrastructure.Curtain;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts._Infrastructure.StateMachine.States
{
  public sealed class StateLoadGameScene : IEnterState, IExitState
  {
    private readonly ISceneLoaderService _sceneLoaderService;
    private readonly ILoadingCurtainService _loadingCurtain;

    public StateLoadGameScene(ISceneLoaderService sceneLoaderService, ILoadingCurtainService loadingCurtain)
    {
      _sceneLoaderService = sceneLoaderService;
      _loadingCurtain = loadingCurtain;
    }

    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      await _sceneLoaderService.Load(Scenes.GAMEPLAY);
      gameStateMachine.Enter<StateGameplayLoop>();
    }

    UniTask IExitState.Exit(IGameStateMachine gameStateMachine)
    {
      _loadingCurtain.Hide();
      
      return UniTask.CompletedTask;
    }
  }
}