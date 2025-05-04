using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using R3;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Systems
{
  public class BulletLifetimeSystem : SystemComponent<BulletComponent>
  {
    
    protected override void OnEnableComponent(BulletComponent bullet)
    {
      base.OnEnableComponent(bullet);

      bullet.OnDestroy
        .First()
        .Subscribe(_ => bullet.Remove())
        .AddTo(bullet.LifetimeDisposable);
    }

    protected override void OnUpdate()
    {
      base.OnUpdate();
            
      Components.Foreach(DestroyBulletAfterTime);
    }
    
    private void DestroyBulletAfterTime(BulletComponent bullet)
    {
      bullet.LifeTime -= Time.deltaTime;

      if (bullet.LifeTime < 0f)
      {
        bullet.Remove();
      }
    }
  }
}