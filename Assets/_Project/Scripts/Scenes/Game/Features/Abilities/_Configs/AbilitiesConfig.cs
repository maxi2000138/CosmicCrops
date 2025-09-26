using System.Collections.Generic;
using _Project.Scripts.Game.Features.Abilities._Configs.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Abilities._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class AbilitiesConfig : BaseConfig<string, AbilityData>
  {
    public override string ConfigName => "Abilities";
    protected override string GetKey(AbilityData data) => data.Id;

    protected override AbilityData ParseData(List<string> row)
    {
      return new()
      {
        Id = row[0],
        Effects = StringParseUtils.ToStringArray(row[1]),
        Statuses = StringParseUtils.ToStringArray(row[2]),
      };
    }
  }
}