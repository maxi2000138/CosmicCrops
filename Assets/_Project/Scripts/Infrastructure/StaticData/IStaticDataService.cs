using _Project.Scripts.Infrastructure.Logger._Configs;

namespace _Project.Scripts.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        LoggerPreset LoggerPreset();
    }
}