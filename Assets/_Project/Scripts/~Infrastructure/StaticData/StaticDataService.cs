using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.StaticData.Data;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.StaticData
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StaticDataService : IStaticDataService
    {
        private const string DataFolder = "StaticData/";
        
        private readonly IAssetService _assetService;
        
        private LoggerData _loggerData;
        private UIData _uiData;

        public StaticDataService(IAssetService assetService)
        {
            _assetService = assetService;
            
        }

        void IStaticDataService.Load()
        {
             _uiData = LoadData<UIData>();
        }
        
        UIData IStaticDataService.UIdata() => _uiData;
        LoggerData IStaticDataService.LoggerData() => _loggerData ??= LoadData<LoggerData>();

        private T LoadData<T>() where T : UnityEngine.Object => _assetService.LoadFromResources<T>(DataFolder + typeof(T).Name);
    }
}