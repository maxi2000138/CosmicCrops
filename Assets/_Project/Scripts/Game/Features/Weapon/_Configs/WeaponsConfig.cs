using System.Collections.Generic;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Weapon._Configs
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
        Prefab = StringParseUtils.ToPrefab(row[2]),
        ClipCount = StringParseUtils.ToInt(row[3]),
        RechargeTime = StringParseUtils.ToFloat(row[4]),
        FireInterval = StringParseUtils.ToFloat(row[5]),
        DetectionDistance = StringParseUtils.ToFloat(row[6]),
        AttackDistance = StringParseUtils.ToFloat(row[7]),
        Aiming = StringParseUtils.ToFloat(row[8]),
        ForceBullet = StringParseUtils.ToFloat(row[9]),
      };
    }
  }

}