using _Project.Scripts._Infrastructure.StaticData.Data;
using CodeBase.Infrastructure.StaticData.Data;

namespace _Project.Scripts._Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        UIData UIdata();
        LevelData LevelData();
        LoggerData LoggerData();
        CharacterData CharacterData();
        void Load();
    }
}