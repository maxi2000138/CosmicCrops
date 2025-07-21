using _Project.Scripts.Game.Features.Units.Enemy._Presets;
using _Project.Scripts.Infrastructure.Logger._Configs;
using _Project.Scripts.Menu.Infrastructure._Presets;

namespace _Project.Scripts.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        LoggerPreset LoggerPreset();
        UnitAnimatorsPreset UnitAnimatorsPreset();
        TexturePreset TexturePreset();
    }
}