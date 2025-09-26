using System;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Game._Editor
{
  public static class EditorConfigHelper
  {
    
    public static T LoadConfig<T>() where T : IConfigParser
    {
      return (T)LoadConfig(typeof(T));
    }

    
    public static IConfigParser LoadConfig(Type type)
    {
      var parser = (IConfigParser)Activator.CreateInstance(type);
      LoadConfigInEditor(parser);
      return parser;
    }
    
    public static void LoadConfigInEditor(IConfigParser config)
    {
      var textAssetTask = Addressables.LoadAssetAsync<TextAsset>(config.ConfigName);
      var textAsset = textAssetTask.WaitForCompletion();

      var data = TsvHelper.ParseTsv(textAsset.text);
      data.RemoveAt(0);

      config.Parse(data);
    }
  }
}