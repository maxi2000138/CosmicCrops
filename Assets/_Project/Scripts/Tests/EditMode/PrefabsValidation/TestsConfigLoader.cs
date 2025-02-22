using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Infrastructure.StaticData.Configs.Data;
using _Project.Scripts.Utils.Parse;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TestsConfigLoader
{
  public async UniTask<List<ConfigPrefab>> GetPrefabsFromConfig(Type configType)
  {
    var config = await LoadConfig(configType);;
    
    var dataValues = config is BaseConfigConstants
      ? new[] { config }
      : GetDataValues(config);

    return GetPrefabNamesFromData(dataValues);
  }

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

  private IEnumerable GetDataValues(IConfigParser config)
  {
    var dataProperty = config.GetType().GetProperty("Data");
    if (dataProperty == null)
      throw new InvalidOperationException("Property 'Data' not found.");

    var dataDictionary = dataProperty.GetValue(config);

    var valuesProperty = dataDictionary.GetType().GetProperty("Values");
    if (valuesProperty == null)
      throw new InvalidOperationException("Property 'Values' not found.");

    return (IEnumerable)valuesProperty.GetValue(dataDictionary);
  }

  private List<ConfigPrefab> GetPrefabNamesFromData(IEnumerable dataValues)
  {
    var prefabTypes = new List<ConfigPrefab>();

    foreach (var data in dataValues)
    {
      var prefabAccessors = GetPrefabAccessors(data.GetType());

      foreach (var accessor in prefabAccessors)
      {
        if (accessor(data) is ConfigPrefab prefab)
        {
          prefabTypes.Add(prefab);
        }
      }
    }

    return prefabTypes;
  }

  private List<Func<object, ConfigPrefab>> GetPrefabAccessors(Type type)
  {
    var accessors = new List<Func<object, ConfigPrefab>>();

    foreach (var field in type.GetFields().Where(f => f.FieldType == typeof(ConfigPrefab)))
    {
      var param = Expression.Parameter(typeof(object), "obj");
      var fieldAccess = Expression.Field(Expression.Convert(param, type), field);
      var lambda = Expression.Lambda<Func<object, ConfigPrefab>>(fieldAccess, param);
      accessors.Add(lambda.Compile());
    }

    foreach (var property in type.GetProperties().Where(p => p.PropertyType == typeof(ConfigPrefab)))
    {
      var param = Expression.Parameter(typeof(object), "obj");
      var propertyAccess = Expression.Property(Expression.Convert(param, type), property);
      var lambda = Expression.Lambda<Func<object, ConfigPrefab>>(propertyAccess, param);
      accessors.Add(lambda.Compile());
    }

    return accessors;
  }
}