using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Entities.Unit.Components
{
  public class UnitComponent : MonoComponent<UnitComponent>, IEnemy 
  {
    [SerializeField] private AgentComponent _agent;
    [SerializeField] private StateMachineComponent _stateMachine;
    [SerializeField] private AnimatorComponent _animator;
    
    public AgentComponent Agent => _agent;
    public AnimatorComponent Animator => _animator;
    public StateMachineComponent StateMachine => _stateMachine;
  }
}