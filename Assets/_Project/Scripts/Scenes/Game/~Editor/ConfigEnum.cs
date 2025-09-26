using _Project.Scripts.Game.Features.Units._Configs;
using _Project.Scripts.Game.Features.Weapon._Configs;
using Sirenix.OdinInspector;

namespace _Project.Scripts.Game._Editor
{
  public static class ConfigEnum
  {
    public const string Weapons = "@" + nameof(ConfigEnum) + "." + nameof(WeaponNames);
    public const string Skins = "@" + nameof(ConfigEnum) + "." + nameof(SkinNames);
    public static ValueDropdownList<int> WeaponNames => GetWeaponsNames();
    public static ValueDropdownList<int> SkinNames => GetSkinsNames();
    

    private static ValueDropdownList<int> GetWeaponsNames()
    {
      var weaponsConfig = EditorConfigHelper.LoadConfig<WeaponsConfig>();

      ValueDropdownList<int> list = new ValueDropdownList<int>();
      foreach (var weapon in weaponsConfig.Data) 
        list.Add(weapon.Value.Weapon, weapon.Key);
      
      return list;
    }
    
    private static ValueDropdownList<int> GetSkinsNames()
    {
      var skinsConfig = EditorConfigHelper.LoadConfig<SkinsConfig>();

      ValueDropdownList<int> list = new ValueDropdownList<int>();
      foreach (var weapon in skinsConfig.Data) 
        list.Add(weapon.Value.Skin, weapon.Key);
      
      return list;
    }
  }
}