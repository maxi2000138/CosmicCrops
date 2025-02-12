using _Project.Scripts.Game.Collector.Components;
using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Entities.Character.Components
{
  public class CharacterComponent : MonoComponent<CharacterComponent>, ICharacter
  {
    [SerializeField] private AnimatorComponent _animator;
    [SerializeField] private CharacterControllerComponent _characterController;
    [SerializeField] private CollectorComponent _collector;
    [SerializeField] private StateMachineComponent _stateMachine;


    public AnimatorComponent Animator => _animator;
    public CharacterControllerComponent CharacterController => _characterController;
    public CollectorComponent Collector => _collector;
    public StateMachineComponent StateMachine => _stateMachine;
    public Vector3 Position => transform.position;
    
    public float Height => 3f;
    public float Scale => 1.5f;
  }
}