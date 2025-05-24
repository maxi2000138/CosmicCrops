using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Editor
{
  public class Tools
  {
    [MenuItem("Tools/Clear Addressables Cache")]
    public static void ShowWindow()
    {
      Caching.ClearCache();
    }
  }

}