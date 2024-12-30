using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.Scopes;
using _Project.Scripts.Infrastructure.StateMachine.Machine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  public sealed class StateLoadGameScene : IEnterLoadState<int>
  {
    private readonly GameStateMachine _gameStateMachine;

    private ISceneLoaderService _sceneLoaderService;

    public StateLoadGameScene(GameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
    }

    [Inject]
    private void Construct(ISceneLoaderService sceneLoaderService)
    {
      _sceneLoaderService = sceneLoaderService;
    }

    async UniTask IEnterLoadState<int>.Enter(int level)
    {
      await _sceneLoaderService.Load(Scenes.GAME);
      Object.FindAnyObjectByType<GameScope>().SetupAndBuild(level);
    }
  
    UniTask IExitState.Exit()
    {
      return UniTask.CompletedTask;
    }
  }
}