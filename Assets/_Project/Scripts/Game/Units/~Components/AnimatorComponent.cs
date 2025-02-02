using _Project.Scripts.Infrastructure.Systems.Components;
using R3;
using UnityEngine;

namespace _Project.Scripts.Game.Units._Components
{
  public sealed class AnimatorComponent : MonoComponent<AnimatorComponent>
  {
    [SerializeField] private Animator _animator;
        
    public Animator Animator => _animator;

    public readonly ReactiveCommand<float> OnRun = new();
    public readonly ReactiveCommand OnCollect = new();
    public readonly ReactiveCommand OnDeath = new();
    public readonly ReactiveCommand OnVictory = new();
  }
}