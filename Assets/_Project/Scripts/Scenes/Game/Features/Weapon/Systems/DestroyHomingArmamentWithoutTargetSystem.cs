using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using R3;

namespace _Project.Scripts.Game.Features.Weapon.Systems
{
  public class DestroyHomingArmamentWithoutTargetSystem : SystemComponent<HomingArmamentComponent>
  {
    protected override void OnUpdate()
    {
      base.OnUpdate();
            
      Components.Foreach(DestroyHomingArmamentWithoutTarget);
    }
    
    private void DestroyHomingArmamentWithoutTarget(HomingArmamentComponent armament)
    {
      if (armament.Unit != null)
      {
        armament.Armament.OnDestroy.Execute(Unit.Default);
      }
    }
  }
}