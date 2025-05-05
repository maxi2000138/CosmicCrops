using _Project.Scripts.Infrastructure.StaticData.Configs.Data;

namespace _Project.Scripts.Game.Features.Weapon._Configs.Data
{
  public class ProjectileData
  {
    public ProjectileType ProjectileType;
    public int CollisionMask;
    public float CollisionRadius;
    public float LifeTime;
    public ConfigPrefab Prefab;
  }
}