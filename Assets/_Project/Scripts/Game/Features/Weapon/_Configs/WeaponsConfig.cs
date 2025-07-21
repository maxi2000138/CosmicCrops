  using System.Collections.Generic;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Weapon._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class WeaponsConfig : BaseConfig<int, WeaponCharacteristicData>
  {
    public override string ConfigName => "Weapons";
    protected override int GetKey(WeaponCharacteristicData data) => data.Id;

    protected override WeaponCharacteristicData ParseData(List<string> row)
    {
      return new()
      {
        Id = StringParseUtils.ToInt(row[0]),
        Weapon = row[1],
        WeaponType = StringParseUtils.ToEnum<WeaponType>(row[2]),
        Ability = row[3],
        WeaponPrefab = StringParseUtils.ToPrefab(row[4]),
        Armament = row[5],
        ClipCount = StringParseUtils.ToInt(row[6]),
        RechargeTime = StringParseUtils.ToFloat(row[7]),
        FireInterval = StringParseUtils.ToFloat(row[8]),
        DetectionDistance = StringParseUtils.ToFloat(row[9]),
        AttackDistance = StringParseUtils.ToFloat(row[10]),
        Aiming = StringParseUtils.ToFloat(row[11]),
        ForceBullet = StringParseUtils.ToFloat(row[12]),
      };
    }
  }
}