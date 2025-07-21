using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure.Logger;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts.Infrastructure.AssetData
{
  public class AssetDownloadService : IAssetDownloadService
  {
    public const string RemoteLabel = "remote";
    
    private readonly IAssetDownloadReporter _assetDownloadReporter;
    private long _downloadSize;

    public float DownloadSizeMb => SizeToMb(_downloadSize);

    public AssetDownloadService(IAssetDownloadReporter assetDownloadReporter)
    {
      _assetDownloadReporter = assetDownloadReporter;
    }
    
    public async UniTask InitializeDownloadDataAsync()
    {
      try
      {
        await Addressables.InitializeAsync().ToUniTask();
        await UpdateCatalogsAsync();
        await UpdateDownloadSizeAsync();
      }
      catch (Exception e)
      {
        DebugLogger.LogError(e.Message, LogsType.Addressables);
      }
    }

    public async UniTask UpdateContentAsync()
    {
      AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync(RemoteLabel);

      while (downloadHandle.IsDone == false && downloadHandle.IsValid())
      {
        await UniTask.Delay(100);
        _assetDownloadReporter.Report(downloadHandle.GetDownloadStatus().Percent);
      }
      _assetDownloadReporter.Report(1f);

      if (downloadHandle.Status == AsyncOperationStatus.Failed) 
        DebugLogger.LogError("Error while loading catalog dependencies", LogsType.Addressables);
      
      if(downloadHandle.IsValid())
        Addressables.Release(downloadHandle);
      
      _assetDownloadReporter.Reset();
    }
    
    private async UniTask UpdateCatalogsAsync()
    {
      List<string> catalogsToUpdate = await Addressables.CheckForCatalogUpdates().ToUniTask();
      if (catalogsToUpdate.IsNullOrEmpty())
        return;

      await Addressables.UpdateCatalogs(catalogsToUpdate).ToUniTask();
    }

    private async UniTask UpdateDownloadSizeAsync() => 
      _downloadSize = await Addressables
        .GetDownloadSizeAsync(RemoteLabel)
        .ToUniTask();

    private float SizeToMb(long downloadSize) => downloadSize * 1f /  1048576;
  }
}