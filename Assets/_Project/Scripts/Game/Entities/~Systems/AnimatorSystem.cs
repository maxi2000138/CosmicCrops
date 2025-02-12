using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils;
using R3;

namespace _Project.Scripts.Game.Entities._Systems
{
  public sealed class AnimatorSystem : SystemComponent<AnimatorComponent>
  {
    protected override void OnEnableComponent(AnimatorComponent component)
    {
      base.OnEnableComponent(component);

      component.OnRun
        .Subscribe(delta => component.Animator.SetFloat(Animations.Velocity, delta))
        .AddTo(component.LifetimeDisposable);

      component.OnCollect
        .Subscribe(_ => component.Animator.SetTrigger(Animations.Collect))
        .AddTo(component.LifetimeDisposable);

      component.OnDeath
        .Subscribe(_ => component.Animator.SetTrigger(Animations.Death))
        .AddTo(component.LifetimeDisposable);
            
      component.OnVictory
        .Subscribe(_ => component.Animator.SetTrigger(Animations.Victory))
        .AddTo(component.LifetimeDisposable);
    }
  }
}
