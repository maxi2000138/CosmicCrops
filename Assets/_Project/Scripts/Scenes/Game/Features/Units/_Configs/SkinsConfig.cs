using System.Collections.Generic;
using _Project.Scripts.Game.Features.Units._Configs.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;

namespace _Project.Scripts.Game.Features.Units._Configs
{
  public class SkinsConfig : BaseConfig<string, SkinCharacteristicData>
  {
    public override string ConfigName => "Skins";
    protected override string GetKey(SkinCharacteristicData data) => data.Skin;
    protected override SkinCharacteristicData ParseData(List<string> row)
    {
      return new()
      {
        Skin = row[0],
        BaseHealth = StringParseUtils.ToFloat(row[1]),
        BaseSpeed = StringParseUtils.ToFloat(row[2]),
      };
    }
  }
}