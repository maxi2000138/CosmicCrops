using _Project.Scripts._Infrastructure.StaticData.Data;

namespace _Project.Scripts._Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        LoggerData LoggerData();
        UIData UIdata();
    }
}