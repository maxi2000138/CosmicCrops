using _Project.Scripts.Infrastructure.StaticData.Data;
using _Project.Scripts.UI.Screens;

namespace _Project.Scripts.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        UIConfig UIConfig();
        LevelConfig LevelConfig();
        LoggerConfig LoggerConfig();
        CharacterConfig CharacterConfig();
        void Load();
        ScreenData ScreenData(ScreenType type);
        LootConfig LootConfig();
        UnitConfig UnitConfig();
        WeaponCharacteristicConfig WeaponCharacteristicConfig();
    }
}