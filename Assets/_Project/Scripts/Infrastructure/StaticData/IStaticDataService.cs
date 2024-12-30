using _Project.Scripts.Infrastructure.StaticData.Data;

namespace _Project.Scripts.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        LoggerData LoggerData();
    }
}