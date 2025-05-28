using _Project.Scripts.Game.Entities.Enemy._Presets;
using _Project.Scripts.Infrastructure.Logger._Configs;

namespace _Project.Scripts.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        LoggerPreset LoggerPreset();
        UnitAnimatorsPreset UnitAnimatorsPreset();
    }
}