using System.Collections.Generic;
using _Project.Scripts._Infrastructure.AssetData;
using _Project.Scripts._Infrastructure.StaticData.Data;
using _Project.Scripts.UI.Screens;
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
        private ScreenData _screenData;

        public StaticDataService(IAssetService assetService)
        {
            _assetService = assetService;
        }

        void IStaticDataService.Load()
        {
             _uiData = LoadData<UIData>();
             _levelData = LoadData<LevelData>();
             _characterData = LoadData<CharacterData>();
             _characterData = LoadData<CharacterData>();
             _screenData = LoadData<ScreenData>();
             _loggerData ??= LoadData<LoggerData>();
        }
        
        ScreenInfo IStaticDataService.ScreenData(ScreenType type) => 
            _screenData.Screens.GetValueOrDefault(type);
        
        UIData IStaticDataService.UIdata() => _uiData;
        LevelData IStaticDataService.LevelData() => _levelData;
        CharacterData IStaticDataService.CharacterData() => _characterData;
        LoggerData IStaticDataService.LoggerData() => _loggerData ??= LoadData<LoggerData>();

        private T LoadData<T>() where T : UnityEngine.Object => _assetService.LoadFromResources<T>(DataFolder + typeof(T).Name);
    }
}