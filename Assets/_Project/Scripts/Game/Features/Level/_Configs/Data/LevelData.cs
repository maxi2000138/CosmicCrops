using _Project.Scripts.Infrastructure.StaticData.Configs.Data;

namespace _Project.Scripts.Game.Features.Level.Data
{
  [System.Serializable]
  public struct LevelData
  {
    public int Level;
    public ConfigPrefab Prefab;
  }
}