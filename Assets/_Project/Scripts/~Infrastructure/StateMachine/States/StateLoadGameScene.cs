using _Project.Scripts._Infrastructure.Curtain;
using _Project.Scripts._Infrastructure.Factories.Game;
using _Project.Scripts._Infrastructure.SceneLoader;
using _Project.Scripts._Infrastructure.StateMachine.Machine;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts._Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateLoadGameScene : IEnterState, IExitState
  {
    private readonly ISceneLoaderService _sceneLoaderService;
    private readonly ILoadingCurtainService _loadingCurtain;
    private readonly IGameFactory _gameFactory;

    public StateLoadGameScene(ISceneLoaderService sceneLoaderService, ILoadingCurtainService loadingCurtain, IGameFactory gameFactory)
    {
      _sceneLoaderService = sceneLoaderService;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
    }

    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      await _sceneLoaderService.Load(Scenes.GAMEPLAY);
      gameStateMachine.Enter<StateGameplayLoop>();
    }

    async UniTask IExitState.Exit(IGameStateMachine gameStateMachine)
    {
      var level = await _gameFactory.CreateLevel();
      var character = await _gameFactory.CreateCharacter(Vector3.zero, null);

      _loadingCurtain.Hide();
    }
  }
}