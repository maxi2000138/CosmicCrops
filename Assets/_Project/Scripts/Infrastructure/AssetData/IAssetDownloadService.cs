using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.AssetData
{
  public interface IAssetDownloadService
  {
    float DownloadSizeMb { get; }
    UniTask InitializeDownloadDataAsync();
    UniTask UpdateContentAsync();
  }
}