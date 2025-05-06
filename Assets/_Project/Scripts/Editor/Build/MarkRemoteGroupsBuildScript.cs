using _Project.Scripts.Infrastructure.AssetData;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Build.DataBuilders;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace _Project.Scripts.Editor.Build
{
  [CreateAssetMenu(fileName = "MarkRemoteGroupsBuildPacked.asset", menuName = "Addressables/Mark Remote Groups Build Script")]
  public class MarkRemoteGroupsBuildScript : BuildScriptPackedMode 
  {
    public override string Name => "Mark Remote Groups Build Script";

    private AddressableAssetSettings Settings => AddressableAssetSettingsDefaultObject.Settings;

    protected override TResult BuildDataImplementation<TResult>(AddressablesDataBuilderInput builderInput)
    {
      TResult result = base.BuildDataImplementation<TResult>(builderInput);

      MarkRemoteAssetsInGroups();
      
      return result;
    }
    
    private void MarkRemoteAssetsInGroups()
    {
      AddRemoteLabelIfNeeded();

      foreach (AddressableAssetGroup group in Settings.groups)
      {
        foreach (AddressableAssetEntry entry in group.entries)
          entry.SetLabel(AssetDownloadService.RemoteLabel, enable: group.IsRemote());
        
        EditorUtility.SetDirty(group);
      }
    }
    
    private void AddRemoteLabelIfNeeded()
    {
      if(Settings.GetLabels().Contains(AssetDownloadService.RemoteLabel) == false)
        Settings.AddLabel(AssetDownloadService.RemoteLabel);
    }
  }
}