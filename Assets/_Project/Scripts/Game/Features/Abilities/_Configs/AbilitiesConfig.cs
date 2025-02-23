using System.Collections.Generic;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Abilities._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class AbilitiesConfig : BaseConfig<string, AbilityData>
  {
    public override string ConfigName => "Abilities";
    protected override string GetKey(AbilityData data) => data.id;

    protected override AbilityData ParseData(List<string> row)
    {
      return new()
      {
        id = row[0],
        effects = StringParseUtils.ToStringArray(row[1]),
        statuses = StringParseUtils.ToStringArray(row[2]),
      };
    }
  }
}