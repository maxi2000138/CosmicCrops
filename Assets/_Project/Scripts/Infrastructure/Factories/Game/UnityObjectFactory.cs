using _Project.Scripts.Infrastructure.UniqueId;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factories.Game
{
  public static class UnityObjectFactory
  {
    public static T Instantiate<T>(T prefab) where T : Object
    {
      var instance = UnityEngine.Object.Instantiate(prefab);
      instance.name = UniqueName(prefab.name);
      
      return instance;
    }

    public static T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Object
    {
      var instance = UnityEngine.Object.Instantiate(prefab, position, rotation, parent);
      instance.name = UniqueName(prefab.name);
      
      return instance;
    }

    private static string UniqueName(string name) => $"{name}_{GameUniqueId.GetNextID()}";
  }
}