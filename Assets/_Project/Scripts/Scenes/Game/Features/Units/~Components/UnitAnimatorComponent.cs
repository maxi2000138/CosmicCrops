using _Project.Scripts.Game.Features.Units.Enemy;
using _Project.Scripts.Game.UI.Pause.Interface;
using _Project.Scripts.Infrastructure.Systems.Components;
using R3;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Units._Components
{
  public sealed class UnitAnimatorComponent : MonoComponent<UnitAnimatorComponent>, IPause
  {
    [SerializeField] private AnimatorWrapper _animatorWrapper;
        
    public AnimatorWrapper AnimatorWrapper => _animatorWrapper;
    
    
    public readonly ReactiveCommand<float> OnRun = new();
    public readonly ReactiveCommand OnCollect = new();
    public readonly ReactiveCommand<float> OnAttack = new();
    public readonly ReactiveCommand OnDeath = new();
    public readonly ReactiveCommand OnVictory = new();
    
    public void SetAnimatorController(RuntimeAnimatorController animatorController)
    {
      _animatorWrapper.SetAnimatorController(animatorController);
    }
    public void Pause(bool isPause)
    {
      _animatorWrapper.SetAnimatorSpeed(isPause ? 0f : 1f);
    }
  }
}