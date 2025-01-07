using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts._Infrastructure.StaticData.Data
{
  [CreateAssetMenu(fileName = nameof(LevelData), menuName = "Data/" + nameof(LevelData))]
  public sealed class LevelData : ScriptableObject
  {
    public Level[] Levels;
  }

  [System.Serializable]
  public struct Level
  {
    public AssetReference PrefabReference;
  }
}