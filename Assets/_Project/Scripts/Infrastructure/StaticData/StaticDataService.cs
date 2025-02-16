using System.Collections.Generic;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.StaticData.Data;
using _Project.Scripts.UI.Screens;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.StaticData
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StaticDataService : IStaticDataService
    {
        private const string DataFolder = "StaticData/";
        
        private readonly IAssetService _assetService;

        private WeaponCharacteristicConfig _weaponCharacteristicConfig;
        private CharacterConfig _characterConfig;
        private LoggerConfig _loggerConfig;
        private ScreenConfig _screenConfig;
        private LevelConfig _levelConfig;
        private UnitConfig _unitConfig;
        private LootConfig _lootConfig;
        private UIConfig _uiConfig;

        public StaticDataService(IAssetService assetService)
        {
            _assetService = assetService;
        }

        void IStaticDataService.Load()
        {
             _lootConfig = LoadConfig<LootConfig>();
             _uiConfig = LoadConfig<UIConfig>();
             _levelConfig = LoadConfig<LevelConfig>();
             _characterConfig = LoadConfig<CharacterConfig>();
             _screenConfig = LoadConfig<ScreenConfig>();
             _unitConfig = LoadConfig<UnitConfig>();
             _weaponCharacteristicConfig = LoadConfig<WeaponCharacteristicConfig>();
        
             _loggerConfig ??= LoadConfig<LoggerConfig>();
        }
        
        ScreenData IStaticDataService.ScreenData(ScreenType type) => 
            _screenConfig.Screens.GetValueOrDefault(type);

        public LootConfig LootConfig() => _lootConfig;
        UIConfig IStaticDataService.UIConfig() => _uiConfig;
        LevelConfig IStaticDataService.LevelConfig() => _levelConfig;
        CharacterConfig IStaticDataService.CharacterConfig() => _characterConfig;
        UnitConfig IStaticDataService.UnitConfig() => _unitConfig;
        WeaponCharacteristicConfig IStaticDataService.WeaponCharacteristicConfig() => _weaponCharacteristicConfig;

        LoggerConfig IStaticDataService.LoggerConfig() => _loggerConfig ??= LoadConfig<LoggerConfig>();

        private T LoadConfig<T>() where T : UnityEngine.Object => _assetService.LoadFromResources<T>(DataFolder + typeof(T).Name);
    }
}