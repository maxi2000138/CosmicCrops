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
    [SerializeField] private UnitAnimatorComponent unitAnimator;
    [SerializeField] private HealthComponent _health;

    public AgentComponent Agent => _agent;
    public UnitAnimatorComponent UnitAnimator => unitAnimator;
    public StateMachineComponent StateMachine => _stateMachine;
    public HealthComponent Health => _health;
    public Vector3 Position => transform.position;
    
    //TODO setup from config
    public float Height => 3f;
    
  }
}