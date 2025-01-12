using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateLoadGameScene : IEnterState, IExitState
  {
    private readonly IGameFactory _gameFactory;
    private readonly ILoadingCurtainService _loadingCurtain;
    private readonly ISceneLoaderService _sceneLoaderService;

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
      
      _loadingCurtain.Hide();
    }
  }
}