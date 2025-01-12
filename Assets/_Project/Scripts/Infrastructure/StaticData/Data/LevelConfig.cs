using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Infrastructure.StaticData.Data
{
  [CreateAssetMenu(fileName = nameof(LevelConfig), menuName = "Config/" + nameof(LevelConfig))]
  public sealed class LevelConfig : ScriptableObject
  {
    public Level[] Levels;
  }

  [System.Serializable]
  public struct Level
  {
    public AssetReference PrefabReference;
  }
}