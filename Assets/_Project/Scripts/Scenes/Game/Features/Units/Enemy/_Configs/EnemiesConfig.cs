using System.Collections.Generic;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Units.Enemy._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class EnemiesConfig : BaseConfig<string, EnemyData>
  {
    public override string ConfigName => "Enemies";
    protected override string GetKey(EnemyData data) => data.Name;
    protected override EnemyData ParseData(List<string> row)
    {
      return new()
      {
        Name = row[0],
        Weapon = row[1],
        Health = StringParseUtils.ToInt(row[2]),
        PatrolRadius = StringParseUtils.ToFloat(row[3]),
        Height = StringParseUtils.ToFloat(row[4]),
        WalkSpeed = StringParseUtils.ToFloat(row[5]),
        PursuitSpeed = StringParseUtils.ToFloat(row[6]),
        PursuitRadius = StringParseUtils.ToFloat(row[7]),
        StayDelay = StringParseUtils.ToFloat(row[8]),
      };
    }
  }

  public class EnemyData
  {
    public string Name;
    public string Weapon;
    public int Health;
    public float PatrolRadius;
    public float Height;
    public float WalkSpeed;
    public float PursuitSpeed;
    public float PursuitRadius;
    public float StayDelay;
  }
}