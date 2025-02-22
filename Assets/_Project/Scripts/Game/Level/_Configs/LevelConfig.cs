using System.Collections.Generic;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Level._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class LevelConfig : BaseConfig<int, LevelData>
  {
    
    public override string ConfigName => "Levels";
    protected override int GetKey(LevelData data) => data.Level;
    
    protected override LevelData ParseData(List<string> row) => new LevelData
    {
      Level = StringParseUtils.ToInt(row[0]),
      Prefab =  StringParseUtils.ToPrefab(row[1]),
    };
  }
}