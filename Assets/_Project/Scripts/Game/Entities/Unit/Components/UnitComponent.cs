using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Game.Entities.Unit._Configs;
using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Weapon.Componets;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Game.Entities.Unit.Components
{
  public class UnitComponent : MonoComponent<UnitComponent>, IEnemy
  {
    [SerializeField] private AgentComponent _agent;
    [SerializeField] private StateMachineComponent _stateMachine;
    [SerializeField] private UnitAnimatorComponent animator;
    [SerializeField] private HealthComponent _health;
    [SerializeField] private WeaponMediatorComponent _weaponMediatorComponent;
    [SerializeField] private UnitAIComponent _unitAiComponent;

    public UnitData Stats { get; private set; }
    public ITarget Target { get; private set; }

    public AgentComponent Agent => _agent;
    public UnitAnimatorComponent Animator => animator;
    public WeaponMediatorComponent WeaponMediator => _weaponMediatorComponent;
    public UnitAIComponent AI => _unitAiComponent;
    public StateMachineComponent StateMachine => _stateMachine;
    public HealthComponent Health => _health;
    public Vector3 Position => transform.position;
    public float Height => Stats.Height;
    public Vector3 Forward => transform.forward;

    public void SetTarget(ITarget target) => Target = target;
    public void SetStats(UnitData stats) => Stats = stats;

    
    public override string ToString() => $"Unit({transform.name})";
  }
}