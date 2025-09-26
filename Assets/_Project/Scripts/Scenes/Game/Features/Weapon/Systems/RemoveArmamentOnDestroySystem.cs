using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using R3;

namespace _Project.Scripts.Game.Features.Weapon.Systems
{
  public class RemoveArmamentOnDestroySystem : SystemComponent<ArmamentComponent>
  {
    protected override void OnEnableComponent(ArmamentComponent armament)
    {
      base.OnEnableComponent(armament);
      
      armament.OnDestroy
        .First()
        .Subscribe(_ => armament.Remove())
        .AddTo(armament.LifetimeDisposable);
    }
  }
}