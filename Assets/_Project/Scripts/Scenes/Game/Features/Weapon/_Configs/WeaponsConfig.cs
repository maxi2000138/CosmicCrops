  using System.Collections.Generic;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Weapon._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class WeaponsConfig : BaseConfig<string, WeaponCharacteristicData>
  {
    public override string ConfigName => "Weapons";
    protected override string GetKey(WeaponCharacteristicData data) => data.Weapon;

    protected override WeaponCharacteristicData ParseData(List<string> row)
    {
      return new()
      {
        Weapon = row[0],
        WeaponType = StringParseUtils.ToEnum<WeaponType>(row[1]),
        Ability = row[2],
        WeaponPrefab = StringParseUtils.ToPrefab(row[3]),
        Armament = row[4],
        ClipCount = StringParseUtils.ToInt(row[5]),
        RechargeTime = StringParseUtils.ToFloat(row[6]),
        FireInterval = StringParseUtils.ToFloat(row[7]),
        DetectionDistance = StringParseUtils.ToFloat(row[8]),
        AttackDistance = StringParseUtils.ToFloat(row[9]),
        Aiming = StringParseUtils.ToFloat(row[10]),
        ForceBullet = StringParseUtils.ToFloat(row[11]),
      };
    }
  }
}