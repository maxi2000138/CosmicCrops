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
  public sealed class StateLoadGameScene : IOverloadedEnterState<int>, IExitState
  {
    private readonly ISceneLoaderService _sceneLoaderService;
    private readonly IStaticDataService _staticData;
    private readonly IAssetService _assetService;
    private readonly UIRootView _uiRootView;


    public StateLoadGameScene(ISceneLoaderService sceneLoaderService, IStaticDataService staticDataService, 
      IAssetService assetService, UIRootView uiRootView)
    {
      _uiRootView = uiRootView;
      _assetService = assetService;
      _staticData = staticDataService;
      _sceneLoaderService = sceneLoaderService;
    }

    async UniTask IOverloadedEnterState<int>.Enter(IGameStateMachine gameStateMachine, int level)
    {
      await _sceneLoaderService.Load(Scenes.GAME);
      
      var uiSceneRootBinder = await _assetService.LoadFromAddressable<GameObject>(_staticData.UIdata().GameplayUIBinder);
      GameObject instance = Object.Instantiate(uiSceneRootBinder);
      
      _uiRootView.AttachSceneUI(instance);

      Object.FindAnyObjectByType<GameScope>().SetupAndBuild(level);
      
      gameStateMachine.Enter<StateGameplayLoop>();
    }

    UniTask IExitState.Exit(IGameStateMachine gameStateMachine)
    {
      _uiRootView.HideLoadingScreen();
      return UniTask.CompletedTask;
    }
  }
}