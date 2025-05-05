using System.Collections.Generic;
using _Project.Scripts.Game.Features.Abilities._Configs.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Abilities._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class EffectsConfig : BaseConfig<string, EffectData>
  {

    public override string ConfigName => "Effects";
    protected override string GetKey(EffectData data) => data.Id;
    protected override EffectData ParseData(List<string> row)
    {

      return new()
      {
        Id = row[0],
        EffectTypeId = StringParseUtils.ToEnum<EffectTypeId>(row[1]),
        Value = StringParseUtils.ToInt(row[2]),
      };
    }
  }

}