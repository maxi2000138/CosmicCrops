using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.Scopes;
using _Project.Scripts.Infrastructure.StateMachine.Machine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  public sealed class StateLoadMenuScene : IEnterState
  {
    private readonly GameStateMachine _gameStateMachine;

    private ISceneLoaderService _sceneLoaderService;

    public StateLoadMenuScene(GameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
    }

    [Inject]
    private void Construct(ISceneLoaderService sceneLoaderService)
    {
      _sceneLoaderService = sceneLoaderService;
    }

    async UniTask IEnterState.Enter()
    {
      await _sceneLoaderService.Load(Scenes.MENU);
      Object.FindAnyObjectByType<MenuScope>().SetupAndBuild();
    }
  
    UniTask IExitState.Exit()
    {
      return UniTask.CompletedTask;
    }
  }
}