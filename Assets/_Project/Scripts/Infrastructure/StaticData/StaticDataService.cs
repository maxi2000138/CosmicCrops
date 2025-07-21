using _Project.Scripts.Game.Features.Units.Enemy._Presets;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Logger._Configs;
using _Project.Scripts.Menu.Features.CharacterPreview._Preset;
using _Project.Scripts.Menu.Infrastructure._Presets;
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
        private TexturePreset _texturePreset;
        private CharacterPreviewPreset _characterPreviewPreset;

        LoggerPreset IStaticDataService.LoggerPreset() => _loggerPreset ??= LoadConfig<LoggerPreset>();
        UnitAnimatorsPreset IStaticDataService.UnitAnimatorsPreset() => _unitAnimatorsPreset;
        TexturePreset IStaticDataService.TexturePreset() => _texturePreset;
        CharacterPreviewPreset IStaticDataService.CharacterPreviewPreset() => _characterPreviewPreset;

        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        void IStaticDataService.Load()
        { 
            _loggerPreset ??= LoadConfig<LoggerPreset>();
            
            _unitAnimatorsPreset = LoadConfig<UnitAnimatorsPreset>();
            _texturePreset = LoadConfig<TexturePreset>();
            _characterPreviewPreset = LoadConfig<CharacterPreviewPreset>();
        }
        

        private T LoadConfig<T>() where T : ScriptableObject => _assetProvider.LoadFromResources<T>(DataFolder + typeof(T).Name);
    }
}