using _Project.Scripts._Infrastructure.AssetData;
using _Project.Scripts._Infrastructure.StaticData.Data;
using CodeBase.Infrastructure.StaticData.Data;
using JetBrains.Annotations;

namespace _Project.Scripts._Infrastructure.StaticData
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StaticDataService : IStaticDataService
    {
        private const string DataFolder = "StaticData/";
        
        private readonly IAssetService _assetService;
        
        private UIData _uiData;
        private LevelData _levelData;
        private CharacterData _characterData;
        private LoggerData _loggerData;

        public StaticDataService(IAssetService assetService)
        {
            _assetService = assetService;
        }

        void IStaticDataService.Load()
        {
             _uiData = LoadData<UIData>();
             _levelData = LoadData<LevelData>();
             _characterData = LoadData<CharacterData>();
             _loggerData ??= LoadData<LoggerData>();
        }
        
        UIData IStaticDataService.UIdata() => _uiData;
        LevelData IStaticDataService.LevelData() => _levelData;
        CharacterData IStaticDataService.CharacterData() => _characterData;
        LoggerData IStaticDataService.LoggerData() => _loggerData ??= LoadData<LoggerData>();

        private T LoadData<T>() where T : UnityEngine.Object => _assetService.LoadFromResources<T>(DataFolder + typeof(T).Name);
    }
}