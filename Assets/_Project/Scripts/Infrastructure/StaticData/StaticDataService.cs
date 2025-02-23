using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Logger._Configs;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.StaticData
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StaticDataService : IStaticDataService
    {
        private const string DataFolder = "StaticData/Presets/";
        
        private readonly IAssetService _assetService;

        private WeaponsConfig _weaponsConfig;
        private LoggerPreset _loggerPreset;

        public StaticDataService(IAssetService assetService)
        {
            _assetService = assetService;
        }

        void IStaticDataService.Load()
        { 
            _loggerPreset ??= LoadConfig<LoggerPreset>();
        }
        
        LoggerPreset IStaticDataService.LoggerPreset() => _loggerPreset ??= LoadConfig<LoggerPreset>();

        private T LoadConfig<T>() where T : ScriptableObject => _assetService.LoadFromResources<T>(DataFolder + typeof(T).Name);
    }
}