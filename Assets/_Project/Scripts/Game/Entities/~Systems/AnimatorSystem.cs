using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils;
using _Project.Scripts.Utils.Constants;
using R3;

namespace _Project.Scripts.Game.Entities._Systems
{
  public sealed class AnimatorSystem : SystemComponent<UnitAnimatorComponent>
  {
    protected override void OnEnableComponent(UnitAnimatorComponent component)
    {
      base.OnEnableComponent(component);

      component.OnRun
        .Subscribe(delta => component.AnimatorWrapper.Animator.SetFloat(Animations.Velocity, delta))
        .AddTo(component.LifetimeDisposable);

      component.OnCollect
        .Subscribe(_ => component.AnimatorWrapper.PlayAnimation(Animations.Collect))
        .AddTo(component.LifetimeDisposable);

      component.OnDeath
        .Subscribe(_ => component.AnimatorWrapper.PlayAnimation(Animations.Death))
        .AddTo(component.LifetimeDisposable);
            
      component.OnVictory
        .Subscribe(_ => component.AnimatorWrapper.PlayAnimation(Animations.Victory))
        .AddTo(component.LifetimeDisposable);

      component.OnAttack
        .Subscribe(interval => {
          component.AnimatorWrapper.SetSpeed(Animations.Attack , Animations.AttackSpeed, interval);
          component.AnimatorWrapper.PlayAnimation(Animations.Attack);
        })
        .AddTo(component.LifetimeDisposable);
    }
  }
}
