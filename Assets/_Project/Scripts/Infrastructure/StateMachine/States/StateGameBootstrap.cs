using _Project.Scripts.Game._Editor;
using _Project.Scripts.Game.Features.AI.Services.AIReporter;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.GUI;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  public class StateGameBootstrap : IEnterState
  {
    private readonly IGameFactory _gameFactory;
    private readonly LevelModel _levelModel;
    private readonly IGuiService _guiService;
    private readonly ICameraService _cameraService;
    private readonly IAssetProvider _assetProvider;
    private readonly IAIReporter _aiReporter;

    public StateGameBootstrap(IGameFactory gameFactory, LevelModel levelModel, IGuiService guiService, 
      ICameraService cameraService, IAssetProvider assetProvider, IAIReporter aiReporter)
    {
      _gameFactory = gameFactory;
      _levelModel = levelModel;
      _guiService = guiService;
      _cameraService = cameraService;
      _assetProvider = assetProvider;
      _aiReporter = aiReporter;
    }
    
    public async UniTask Enter(IGameStateMachine gameStateMachine)
    {
      SetupEditorBridge();

      CleanupWorld();
      await _gameFactory.CreateLevel();
      
      gameStateMachine.Enter<StateLobby>().Forget();
    }

    private void CleanupWorld()
    {
      _assetProvider.Cleanup();
      _guiService.Cleanup();
      _cameraService.Cleanup();
    }
    
    private void SetupEditorBridge() => EditorBridge.Init(_aiReporter, _levelModel);
  }
}