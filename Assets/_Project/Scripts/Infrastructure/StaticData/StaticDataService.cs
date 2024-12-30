using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.StaticData.Data;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.StaticData
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly IAssetService _assetService;
        
        private LoggerData _logger;

        public StaticDataService(IAssetService assetService)
        {
            _assetService = assetService;
            
            
        }

        void IStaticDataService.Load()
        {
            
        }
        
        LoggerData IStaticDataService.LoggerData() => _logger ??= _assetService.LoadFromResources<LoggerData>(AssetAddress.LoggerDataPath);
    }
}