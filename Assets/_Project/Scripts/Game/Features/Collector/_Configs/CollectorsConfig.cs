using System.Collections.Generic;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Collector._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class CollectorsConfig : BaseConfig<CollectorType, CollectorData>
  {
    public override string ConfigName => "Collectors";
    protected override CollectorType GetKey(CollectorData data) => data.CollectorType;
    protected override CollectorData ParseData(List<string> row)
    {
      return new()
      {
        CollectorType = StringParseUtils.ToEnum<CollectorType>(row[0]),
        CollectTime = StringParseUtils.ToFloat(row[1]),
        CollectRadius = StringParseUtils.ToFloat(row[2]),
      };
    }
  }
}