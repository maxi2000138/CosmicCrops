using System.Collections.Generic;
using _Project.Scripts.Game.Entities.Loot.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Entities.Loot._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class LootConfig : BaseConfig<LootType, LootData>
  {
    public override string ConfigName => "Loot";
    protected override LootType GetKey(LootData data) => data.Type;

    protected override LootData ParseData(List<string> row)
    {
      return new()
      {
        Type = StringParseUtils.ToEnum<LootType>(row[0]),
        PrefabName = row[1],
      };
    }
  }

  public class LootData
  {
    public LootType Type;
    public string PrefabName;
  }
}