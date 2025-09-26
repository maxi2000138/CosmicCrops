using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using R3;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Systems
{
  public class DestroyArmamentsByLifetimeSystem : SystemComponent<ArmamentComponent>
  {
    protected override void OnUpdate()
    {
      base.OnUpdate();
            
      Components.Foreach(DestroyBulletAfterTime);
    }
    
    private void DestroyBulletAfterTime(ArmamentComponent armament)
    {
      armament.LifeTime -= Time.deltaTime;

      if (armament.LifeTime < 0f)
      {
        armament.OnDestroy.Execute(Unit.Default);
      }
    }
  }

}