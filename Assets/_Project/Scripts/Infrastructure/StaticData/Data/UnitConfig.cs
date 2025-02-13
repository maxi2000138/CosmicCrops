using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Infrastructure.StaticData.Data
{
  [CreateAssetMenu(fileName = nameof(UnitConfig), menuName = "Config/" + nameof(UnitConfig))]
  public sealed class UnitConfig : ScriptableObject
  {
    public AssetReference PrefabReference;
  }
}