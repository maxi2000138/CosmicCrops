using _Project.Scripts.Game.Collector.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine.Components;
using _Project.Scripts.Game.Units.Character.Interface;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Units.Character.Components
{
  public class CharacterComponent : MonoComponent<CharacterComponent>, ICharacter
  {
    [SerializeField] private CharacterControllerComponent _characterController;
    [SerializeField] private CollectorComponent _collector;
    [SerializeField] private StateMachineComponent _stateMachine;

    
    public CollectorComponent Collector => _collector;
    public CharacterControllerComponent CharacterController => _characterController;
    public StateMachineComponent StateMachine => _stateMachine;
    public Vector3 Position => transform.position;
    
    public float Height => 3f;
    public float Scale => 1.5f;
  }
}