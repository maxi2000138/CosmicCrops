using _Project.Scripts.Game.Features.Units._Components;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Constants;
using R3;

namespace _Project.Scripts.Game.Features.Units._Systems
{
  public sealed class UnitAnimatorSystem : SystemComponent<UnitAnimatorComponent>
  {
    protected override void OnEnableComponent(UnitAnimatorComponent armament)
    {
      base.OnEnableComponent(armament);

      armament.OnRun
        .Subscribe(delta => armament.AnimatorWrapper.Animator.SetFloat(Animations.Velocity, delta))
        .AddTo(armament.LifetimeDisposable);

      armament.OnCollect
        .Subscribe(_ => armament.AnimatorWrapper.PlayAnimation(Animations.Collect))
        .AddTo(armament.LifetimeDisposable);

      armament.OnDeath
        .Subscribe(_ => armament.AnimatorWrapper.PlayAnimation(Animations.Death))
        .AddTo(armament.LifetimeDisposable);
            
      armament.OnVictory
        .Subscribe(_ => armament.AnimatorWrapper.PlayAnimation(Animations.Victory))
        .AddTo(armament.LifetimeDisposable);

      armament.OnAttack
        .Subscribe(interval => {
          armament.AnimatorWrapper.SetAnimationSpeed(Animations.Attack , Animations.AttackSpeed, interval);
          armament.AnimatorWrapper.PlayAnimation(Animations.Attack);
        })
        .AddTo(armament.LifetimeDisposable);
    }
  }
}
