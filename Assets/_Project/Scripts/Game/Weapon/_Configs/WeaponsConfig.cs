using System.Collections.Generic;
using _Project.Scripts.Game.Weapon.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Weapon._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class WeaponsConfig : BaseConfig<WeaponType, WeaponCharacteristicData>
  {
    public override string ConfigName => "Weapons";
    protected override WeaponType GetKey(WeaponCharacteristicData data) => data.WeaponType;

    protected override WeaponCharacteristicData ParseData(List<string> row)
    {
      return new()
      {
        WeaponType = StringParseUtils.ToEnum<WeaponType>(row[0]),
        Ability = row[1],
        ClipCount = StringParseUtils.ToInt(row[2]),
        RechargeTime = StringParseUtils.ToFloat(row[3]),
        FireInterval = StringParseUtils.ToFloat(row[4]),
        DetectionDistance = StringParseUtils.ToFloat(row[5]),
        AttackDistance = StringParseUtils.ToFloat(row[6]),
        Aiming = StringParseUtils.ToFloat(row[7]),
      };
    }
  }
  
  public class WeaponCharacteristicData
  {
    public WeaponType WeaponType;
    
    public string Ability;
    public int ClipCount;
    public float RechargeTime;
    public float FireInterval;
    public float DetectionDistance;
    public float AttackDistance;
    public float Aiming;
  }
}