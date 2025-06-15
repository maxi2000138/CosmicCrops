using System.Collections.Generic;
using _Project.Scripts.Game.Features.Units.Enemy._Configs;
using _Project.Scripts.Utils.Parse;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Utils.Configs
{
  public static class ConfigsUtils
  {
    public static IEnumerable<string> GetAllUnemiesNames()
    {
      EnemiesConfig configParser = new EnemiesConfig();
      var textAsset = Addressables.LoadAssetAsync<TextAsset>(configParser.ConfigName).WaitForCompletion();
      
      var data = TsvHelper.ParseTsv(textAsset.text);
      data.RemoveAt(0);

      configParser.Parse(data);
      
      return configParser.Data.Keys;
    }
  }
}