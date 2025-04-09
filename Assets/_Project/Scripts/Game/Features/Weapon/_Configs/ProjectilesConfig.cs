using System.Collections.Generic;
using _Project.Scripts.Game.Features.Weapon.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Weapon._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class ProjectilesConfig : BaseConfig<ProjectileType, ProjectileData>
  {
    public override string ConfigName => "Projectiles";
    protected override ProjectileType GetKey(ProjectileData data) => data.ProjectileType;
    protected override ProjectileData ParseData(List<string> row)
    {
      return new()
      {
        ProjectileType = StringParseUtils.ToEnum<ProjectileType>(row[0]),
        CollisionRadius = StringParseUtils.ToFloat(row[1]),
        LifeTime = StringParseUtils.ToFloat(row[2]),
        Prefab = StringParseUtils.ToPrefab(row[3]),
      };
    }
  }

}