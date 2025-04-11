using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.GUI;
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
    private readonly LevelModel _levelModel;
    private readonly IGuiService _guiService;
    private readonly ICameraService _cameraService;
    private readonly ILoadingCurtainService _loadingCurtain;
    private readonly ISceneLoaderService _sceneLoaderService;

    public StateLoadGameScene(ISceneLoaderService sceneLoaderService, ILoadingCurtainService loadingCurtain, 
      IGameFactory gameFactory, LevelModel levelModel, IGuiService guiService, ICameraService cameraService)
    {
      _sceneLoaderService = sceneLoaderService;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _levelModel = levelModel;
      _guiService = guiService;
      _cameraService = cameraService;
    }

    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      _loadingCurtain.Show();
      
      CleanupWorld();
      
      await _sceneLoaderService.Load(Scenes.GAMEPLAY);
      gameStateMachine.Enter<StateLobby>().Forget();
    }


    async UniTask IExitState.Exit(IGameStateMachine gameStateMachine)
    {
      await _gameFactory.CreateLevel();
      
      _loadingCurtain.Hide();
    }
    
    private void CleanupWorld()
    {
      _levelModel.Cleanup();
      _guiService.Cleanup();
      _cameraService.Cleanup();
    }
  }
}