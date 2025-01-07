using _Project.Scripts._Infrastructure.StaticData.Data;
using _Project.Scripts.UI.Screens;

namespace _Project.Scripts._Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        UIData UIdata();
        LevelData LevelData();
        LoggerData LoggerData();
        CharacterData CharacterData();
        void Load();
        ScreenInfo ScreenData(ScreenType type);
    }
}