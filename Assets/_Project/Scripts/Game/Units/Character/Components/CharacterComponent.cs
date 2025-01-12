using _Project.Scripts.Game.Infrastructure.StateMachine.Components;
using _Project.Scripts.Game.Units.Character.Interface;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Units.Character.Components
{
  public class CharacterComponent : MonoComponent<CharacterComponent>, ICharacter
  {
    [SerializeField] private CharacterControllerComponent _characterController;
    [SerializeField] private StateMachineComponent _stateMachine;

    
    public CharacterControllerComponent CharacterController => _characterController;
    public StateMachineComponent StateMachine => _stateMachine;
    public Vector3 Position => transform.position;
  }
}