using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Infrastructure.StaticData.Configs.Data;
using _Project.Scripts.Utils.Parse;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ValidationTests
{
  [TestCaseSource(nameof(GetConfigsTypes))]
  public async Task ValidationTest(Type type)
  {
    ConfigLoader configLoader = new ConfigLoader();
    PrefabValidator prefabValidator = new PrefabValidator();
    List<string> missingPrefabs = new List<string>();

    IConfigParser config = await configLoader.LoadConfig(type);

    IEnumerable dataValues;

    if (config is BaseConfigConstants)
    {
      dataValues = new[] { config };
    }
    else
    {
      var dataProperty = config.GetType().GetProperty("Data");
      if (dataProperty == null)
      {
        throw new InvalidOperationException("Property 'Data' not found.");
      }

      var dataDictionary = dataProperty.GetValue(config);

      var valuesProperty = dataDictionary.GetType().GetProperty("Values");
      if (valuesProperty == null)
      {
        throw new InvalidOperationException("Property 'Values' not found.");
      }

      dataValues = (IEnumerable)valuesProperty.GetValue(dataDictionary);
    }
    


    
    var prefabNames = GetPrefabNamesFromData(dataValues);

    prefabNames.Should().BeEmpty();
  }
  
  private List<ConfigPrefab> GetPrefabNamesFromData(IEnumerable dataValues)
  {
    var prefabTypes = new List<ConfigPrefab>();

    foreach (var data in dataValues)
    {
      var fields = data.GetType().GetFields()
        .Where(prop => prop.FieldType == typeof(ConfigPrefab));

      var properties = data.GetType().GetProperties()
        .Where(prop => prop.PropertyType == typeof(ConfigPrefab));

      foreach (var property in properties)
      {
        if (property.GetValue(data) is ConfigPrefab prefab)
        {
          prefabTypes.Add(prefab);
        }
      }
      
      foreach (var field in fields)
      {
        if (field.GetValue(data) is ConfigPrefab prefab)
        {
          prefabTypes.Add(prefab);
        }
      }
    }

    return prefabTypes;
  }
  

  public static IEnumerable<Type> GetConfigsTypes()
  {
    var interfaceType = typeof(IConfigParser);

    if (!interfaceType.IsInterface)
    {
      throw new ArgumentException($"Type {interfaceType.Name} is not an interface.");
    }

    var assemblies = AppDomain.CurrentDomain.GetAssemblies();

    foreach (var assembly in assemblies)
    {
      foreach (var type in assembly.GetTypes())
      {
        if (interfaceType.IsAssignableFrom(type)
            && !type.IsAbstract)
        {
          yield return type;
        }
      }
    }
  }

  private class ConfigLoader
  {
    public async UniTask<IConfigParser> LoadConfig(Type type)
    {
      var parser = (IConfigParser)Activator.CreateInstance(type);
      return await LoadConfig(parser, parser.ConfigName);
    }
    
    public async UniTask<T> LoadConfig<T>(T parser, string name) where T : IConfigParser
    {
      var textAsset = await Addressables.LoadAssetAsync<TextAsset>(name).ToUniTask();

      var data = TsvHelper.ParseTsv(textAsset.text);
      data.RemoveAt(0);

      parser.Parse(data);

      return parser;
    }
  }

  private class PrefabValidator
  {
    public bool ExistsInAdressables(string assetName) => false;
  }
}