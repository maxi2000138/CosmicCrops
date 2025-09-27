using _Project.Scripts.Game.Features.Units._Configs;
using _Project.Scripts.Game.Features.Weapon._Configs;
using Sirenix.OdinInspector;

namespace _Project.Scripts.Game._Editor
{
  public static class ConfigEnum
  {
    public const string Weapons = "@" + nameof(ConfigEnum) + "." + nameof(WeaponNames);
    public const string Skins = "@" + nameof(ConfigEnum) + "." + nameof(SkinNames);
    public static ValueDropdownList<string> WeaponNames => GetWeaponsNames();
    public static ValueDropdownList<string> SkinNames => GetSkinsNames();
    

    private static ValueDropdownList<string> GetWeaponsNames()
    {
      var weaponsConfig = EditorConfigHelper.LoadConfig<WeaponsConfig>();

      ValueDropdownList<string> list = new ValueDropdownList<string>();
      foreach (var weapon in weaponsConfig.Data) 
        list.Add(weapon.Value.Weapon, weapon.Key);
      
      return list;
    }
    
    private static ValueDropdownList<string> GetSkinsNames()
    {
      var skinsConfig = EditorConfigHelper.LoadConfig<SkinsConfig>();

      ValueDropdownList<string> list = new ValueDropdownList<string>();
      foreach (var weapon in skinsConfig.Data) 
        list.Add(weapon.Value.Skin, weapon.Key);
      
      return list;
    }
  }
}