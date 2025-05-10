using System.Collections.Generic;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Weapon._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class ProjectilesConfig : BaseConfig<string, ProjectileData>
  {
    public override string ConfigName => "Projectiles";
    protected override string GetKey(ProjectileData data) => data.Projectile;
    protected override ProjectileData ParseData(List<string> row)
    {
      return new()
      {
        Projectile = row[0],
        CollisionMask = StringParseUtils.ToCollisionMask(row[1]),
        CollisionRadius = StringParseUtils.ToFloat(row[2]),
        LifeTime = StringParseUtils.ToFloat(row[3]),
        Prefab = StringParseUtils.ToPrefab(row[4]),
      };
    }
  }

}