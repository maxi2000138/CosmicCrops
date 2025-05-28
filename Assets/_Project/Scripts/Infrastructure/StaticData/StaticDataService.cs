using _Project.Scripts.Game.Entities.Enemy._Presets;
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
        
        private readonly IAssetProvider _assetProvider;

        private LoggerPreset _loggerPreset;
        private UnitAnimatorsPreset _unitAnimatorsPreset;

        LoggerPreset IStaticDataService.LoggerPreset() => _loggerPreset ??= LoadConfig<LoggerPreset>();
        UnitAnimatorsPreset IStaticDataService.UnitAnimatorsPreset() => _unitAnimatorsPreset;
        
        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        void IStaticDataService.Load()
        { 
            _loggerPreset ??= LoadConfig<LoggerPreset>();
            
            _unitAnimatorsPreset = LoadConfig<UnitAnimatorsPreset>();
        }
        

        private T LoadConfig<T>() where T : ScriptableObject => _assetProvider.LoadFromResources<T>(DataFolder + typeof(T).Name);
    }
}