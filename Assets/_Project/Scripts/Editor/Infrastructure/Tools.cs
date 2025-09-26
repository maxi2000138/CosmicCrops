using System;
using System.Collections.Generic;
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

  public class BatchRigReplacerWindow : EditorWindow
  {
    private GameObject targetRootGO;
    private Transform newRigRoot;

    [MenuItem("Tools/Batch Skinned Mesh Rig Replacer")]
    public static void ShowWindow()
    {
      GetWindow<BatchRigReplacerWindow>("Batch Rig Replacer");
    }

    private void OnGUI()
    {
      GUILayout.Label("Batch Skinned Mesh Rig Replacer", EditorStyles.boldLabel);

      targetRootGO = EditorGUILayout.ObjectField("Target Root (Meshes)", targetRootGO, typeof(GameObject), true) as GameObject;
      newRigRoot = EditorGUILayout.ObjectField("New Rig Root", newRigRoot, typeof(Transform), true) as Transform;

      GUI.enabled = targetRootGO != null && newRigRoot != null;

      if (GUILayout.Button("Replace Rigs in All SkinnedMeshRenderers"))
      {
        ReplaceAllRigs();
      }

      GUI.enabled = true;
    }

    private void ReplaceAllRigs()
    {
      if (targetRootGO == null || newRigRoot == null)
      {
        Debug.LogError("❌ Missing required input.");
        return;
      }

      var boneMap = new Dictionary<string, Transform>();
      foreach (var t in newRigRoot.GetComponentsInChildren<Transform>())
      {
        boneMap[t.name] = t;
      }

      int changedCount = 0;
      var renderers = targetRootGO.GetComponentsInChildren<SkinnedMeshRenderer>(true);

      foreach (var smr in renderers)
      {
        Undo.RecordObject(smr, "Replace Rig");

        var oldBones = smr.bones;
        var newBones = new Transform[oldBones.Length];

        for (int i = 0; i < oldBones.Length; i++)
        {
          var oldBone = oldBones[i];
          if (oldBone != null && boneMap.TryGetValue(oldBone.name, out var newBone))
          {
            newBones[i] = newBone;
          }
          else
          {
            Debug.LogWarning($"⚠️ Bone '{oldBone?.name}' not found in new rig. Keeping original.");
            newBones[i] = oldBone;
          }
        }

        smr.bones = newBones;

        // try remap root bone
        if (smr.rootBone != null && boneMap.TryGetValue(smr.rootBone.name, out var newRootBone))
        {
          smr.rootBone = newRootBone;
        }

        changedCount++;
      }

      Debug.Log($"✅ Replaced rig for {changedCount} SkinnedMeshRenderer(s).");
    }
  }
}