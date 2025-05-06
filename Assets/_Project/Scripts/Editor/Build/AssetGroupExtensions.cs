using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;

namespace _Project.Scripts.Editor.Build
{
  public static class AssetGroupExtensions
  {
    public static bool IsRemote(this AddressableAssetGroup group)
    {
      BundledAssetGroupSchema schema = group.GetSchema<BundledAssetGroupSchema>();

      if (schema == null)
        return false;

      return schema.BuildPath.GetValue(AddressableAssetSettingsDefaultObject.Settings).Contains("http") ||
             schema.LoadPath.GetValue(AddressableAssetSettingsDefaultObject.Settings).Contains("http");
    }
  }
}