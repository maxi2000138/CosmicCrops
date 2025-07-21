using System.Collections.Generic;
using _Project.Scripts.Game.Features.Units._Configs.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;

namespace _Project.Scripts.Game.Features.Units._Configs
{
  public class SkinsConfig : BaseConfig<int, SkinCharacteristicData>
  {
    public override string ConfigName => "Skins";
    protected override int GetKey(SkinCharacteristicData data) => data.Id;
    protected override SkinCharacteristicData ParseData(List<string> row)
    {
      return new()
      {
        Id = StringParseUtils.ToInt(row[0]),
        Skin = row[1],
        BaseHealth = StringParseUtils.ToFloat(row[2]),
        BaseSpeed = StringParseUtils.ToFloat(row[3]),
      };
    }
  }
}