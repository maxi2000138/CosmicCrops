using System.Collections.Generic;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Entities.Unit._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class UnitsConfig : BaseConfig<string, UnitData>
  {
    public override string ConfigName => "Units";
    protected override string GetKey(UnitData data) => data.Name;
    protected override UnitData ParseData(List<string> row)
    {
      return new()
      {
        Name = row[0],
        PrefabName = row[1],
        Health = StringParseUtils.ToInt(row[2]),
        PatrolRadius = StringParseUtils.ToFloat(row[3]),
        Height = StringParseUtils.ToFloat(row[4]),
        Scale = StringParseUtils.ToFloat(row[5]),
        WalkSpeed = StringParseUtils.ToFloat(row[6]),
        PursuitSpeed = StringParseUtils.ToFloat(row[7]),
        PursuitRadius = StringParseUtils.ToFloat(row[8]),
        StayDelay = StringParseUtils.ToFloat(row[9])
      };
    }
  }

  public class UnitData
  {
    public string Name;
    public string PrefabName;
    public int Health;
    public float PatrolRadius;
    public float Height;
    public float Scale;
    public float WalkSpeed;
    public float PursuitSpeed;
    public float PursuitRadius;
    public float StayDelay;
  }
}