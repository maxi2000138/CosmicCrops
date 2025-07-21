using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
namespace _Project.Scripts.Tests.EditMode.PrefabsValidation
{
  public class PrefabValidator
  {
    public bool ExistsInAdressables(string assetName)
    {
      var assetLocationTask = Addressables.LoadResourceLocationsAsync(assetName);
      var assetLocation = assetLocationTask.WaitForCompletion();
    
      if(assetLocation.Count > 0)
        return true;

      return false;
    }
  }
}