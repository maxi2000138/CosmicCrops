using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts._Infrastructure.StaticData.Data
{
  [CreateAssetMenu(fileName = nameof(UIData), menuName = "Data/" + nameof(UIData))]
  public sealed class UIData : ScriptableObject
  {
    public AssetReference GameplayUIBinder;
    public AssetReference MainMenuUIBinder;
  }
}