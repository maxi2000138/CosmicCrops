using _Project.Scripts._Infrastructure.AssetData;
using _Project.Scripts._Infrastructure.SceneLoader;
using _Project.Scripts._Infrastructure.Scopes;
using _Project.Scripts._Infrastructure.StateMachine.Machine;
using _Project.Scripts._Infrastructure.StaticData;
using _Project.Scripts._Infrastructure.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts._Infrastructure.StateMachine.States
{
  public sealed class StateLoadMenuScene : IEnterState, IExitState
  {
    private readonly ISceneLoaderService _sceneLoaderService;
    private readonly IStaticDataService _staticData;
    private readonly IAssetService _assetService;
    private readonly UIRootView _uiRootView;

    public StateLoadMenuScene(ISceneLoaderService sceneLoaderService, IAssetService assetService, 
      IStaticDataService staticData, UIRootView uiRootView)
    {
      _sceneLoaderService = sceneLoaderService;
      _assetService = assetService;
      _staticData = staticData;
      _uiRootView = uiRootView;
    }

    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      await _sceneLoaderService.Load(Scenes.MENU);
      
      var uiSceneRootBinder = await _assetService.LoadFromAddressable<GameObject>(_staticData.UIdata().MainMenuUIBinder);
      GameObject instance = Object.Instantiate(uiSceneRootBinder);
      
      _uiRootView.AttachSceneUI(instance);
      
      Object.FindAnyObjectByType<MenuScope>().SetupAndBuild();

      gameStateMachine.Enter<StateMenuLoop>();
    }
  
    UniTask IExitState.Exit(IGameStateMachine gameStateMachine)
    {
      _uiRootView.HideLoadingScreen();
      
      return UniTask.CompletedTask;
    }
  }
}