using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Editor.Infrastructure
{
  public class Tools
  {
    [MenuItem("Tools/Clear Addressables Cache")]
    public static void ShowWindow()
    {
      Caching.ClearCache();
    }
  }

  public static class CustomEnumFinder
  {
    [MenuItem("Tools/Find Custom Enums")]
    public static void FindCustomEnums()
    {
      var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
      var customEnumTypes = assemblies
        .Where(assembly => !IsSystemOrUnityAssembly(assembly)) // Исключаем системные сборки
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => type.IsEnum)
        .OrderBy(type => type.FullName)
        .ToList();
        
      Debug.Log($"Found {customEnumTypes.Count} custom enums:");
      foreach (var enumType in customEnumTypes)
      {
        Debug.Log($"{enumType.FullName} (in {enumType.Assembly.GetName().Name})");
      }
    }

    private static bool IsSystemOrUnityAssembly(Assembly assembly)
    {
      string assemblyName = assembly.GetName().Name;
        
      return assemblyName.StartsWith("System") ||
             assemblyName.StartsWith("Microsoft") ||
             assemblyName.StartsWith("Unity") ||
             assemblyName.StartsWith("UnityEngine") ||
             assemblyName.StartsWith("UnityEditor") ||
             assemblyName.StartsWith("mscorlib") ||
             assemblyName.StartsWith("netstandard") ||
             assemblyName.StartsWith("nunit") ||
             assemblyName.StartsWith("Mono.") ||
             assemblyName.StartsWith("ExCSS") ||
             assemblyName.StartsWith("Newtonsoft") ||
             assemblyName.StartsWith("Unity.") ||
             assemblyName.StartsWith("UnityEngine.") ||
             assemblyName.StartsWith("UnityEditor.");
    }
  }
}