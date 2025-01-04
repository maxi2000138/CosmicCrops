using _Project.Scripts._Infrastructure.AssetData;
using _Project.Scripts._Infrastructure.StateMachine.Machine;
using _Project.Scripts._Infrastructure.StaticData;
using _Project.Scripts._Infrastructure.UI;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts._Infrastructure.StateMachine.States
{
  public sealed class StateBootstrap : IEnterState
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IAssetService _assetService;
    private readonly UIRootView _uiRootView;


    public StateBootstrap(IStaticDataService staticDataService, IAssetService assetService, UIRootView uiRootView)
    {
      _uiRootView = uiRootView;
      _assetService = assetService;
      _staticDataService = staticDataService;
    }


    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      _uiRootView.ShowLoadingScreen();
      
      LoadResources();
      await _assetService.Init();
      
      EnterStateLoadProgressState(gameStateMachine);
    }

    private void LoadResources() => _staticDataService.Load();
  

    private void EnterStateLoadProgressState(IGameStateMachine gameStateMachine) => 
      gameStateMachine.Enter<StateLoadProgress>();
  }
}