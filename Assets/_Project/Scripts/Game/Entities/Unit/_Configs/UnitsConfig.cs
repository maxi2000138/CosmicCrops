using System.Collections.Generic;
using _Project.Scripts.Infrastructure.StaticData.Configs;
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
      };
    }
  }

  public class UnitData
  {
    public string Name;
    public string PrefabName;
  }
}