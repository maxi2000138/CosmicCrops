using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Logger;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.StaticData.Configs.Loader;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateLoadStaticData : IEnterState
  {
    private readonly IConfigsLoader _configsLoader;
    private readonly IStaticDataService _staticData;
    private readonly IAssetDownloadService _assetDownloadService;
    private readonly IAssetProvider _assetProvider;
    public StateLoadStaticData(IConfigsLoader configsLoader, IStaticDataService staticData, IAssetDownloadService assetDownloadService)
    {
      _staticData = staticData;
      _assetDownloadService = assetDownloadService;
      _configsLoader = configsLoader;
    }
    
    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      await DownloadAssets();
      await LoadConfigs();
      LoadResources();

      gameStateMachine.Enter<StateLoadProgress>();
    }
    
    private async UniTask DownloadAssets()
    {
      await _assetDownloadService.InitializeDownloadDataAsync();
      
      DebugLogger.LogWarning("Downloaded size is " + _assetDownloadService.DownloadSizeMb + " Mb", LogsType.Addressables);
      
      if (_assetDownloadService.DownloadSizeMb > 0)
        await _assetDownloadService.UpdateContentAsync();
    }
    
    private void LoadResources() => _staticData.Load();
    private UniTask LoadConfigs() => _configsLoader.LoadConfigs(null);
  }


}