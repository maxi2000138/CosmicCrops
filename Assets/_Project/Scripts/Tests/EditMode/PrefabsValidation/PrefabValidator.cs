using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class PrefabValidator
{
  public async UniTask<bool> ExistsInAdressables(string assetName)
  {
    var assetLocation = await Addressables.LoadResourceLocationsAsync(assetName).ToUniTask();
    
    if(assetLocation.Count > 0)
      return true;

    return false;
  }
}