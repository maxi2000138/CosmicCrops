using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.StaticData.Configs;
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
    private readonly IAssetService _assetService;
    public StateLoadStaticData(IConfigsLoader configsLoader, IStaticDataService staticData, IAssetService assetService)
    {
      _staticData = staticData;
      _assetService = assetService;
      _configsLoader = configsLoader;
    }
    
    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      await InitAsset();
      await LoadConfigs();
      LoadResources();

      gameStateMachine.Enter<StateLoadProgress>();
    }

    private void LoadResources() => _staticData.Load();
    private async UniTask InitAsset() => await _assetService.Init();
    private UniTask LoadConfigs() => _configsLoader.LoadConfigs(null);
  }


}