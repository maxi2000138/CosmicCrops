using System.Collections.Generic;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Entities.Enemy._Configs
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
        PrefabName = row[1],
        Weapon = row[2],
        Health = StringParseUtils.ToInt(row[3]),
        PatrolRadius = StringParseUtils.ToFloat(row[4]),
        Height = StringParseUtils.ToFloat(row[5]),
        Scale = StringParseUtils.ToFloat(row[6]),
        WalkSpeed = StringParseUtils.ToFloat(row[7]),
        PursuitSpeed = StringParseUtils.ToFloat(row[8]),
        PursuitRadius = StringParseUtils.ToFloat(row[9]),
        StayDelay = StringParseUtils.ToFloat(row[10])
      };
    }
  }

  public class EnemyData
  {
    public string Name;
    public string PrefabName;
    public string Weapon;
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