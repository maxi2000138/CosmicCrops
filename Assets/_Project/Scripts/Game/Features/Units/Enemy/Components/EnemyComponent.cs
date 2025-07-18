using _Project.Scripts.Game.Features.Units._Components;
using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Game.Features.Units.Enemy._Configs;
using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Game.Features.Units.Enemy.Components
{
  public class EnemyComponent : MonoComponent<EnemyComponent>, IEnemy
  {
    [SerializeField] private AgentComponent _agent;
    [SerializeField] private StateMachineComponent _stateMachine;
    [SerializeField] private UnitAnimatorComponent animator;
    [SerializeField] private HealthComponent _health;
    [SerializeField] private WeaponMediatorComponent _weaponMediatorComponent;
    [SerializeField] private EnemyAIComponent enemyAIComponent;

    public EnemyData Stats { get; private set; }
    public IUnit Target { get; private set; }

    public AgentComponent Agent => _agent;
    public UnitAnimatorComponent Animator => animator;
    public WeaponMediatorComponent WeaponMediator => _weaponMediatorComponent;
    public EnemyAIComponent AI => enemyAIComponent;
    public StateMachineComponent StateMachine => _stateMachine;
    public HealthComponent Health => _health;
    public Vector3 Position => transform.position;
    public float Height => Stats.Height;
    public Vector3 Forward => transform.forward;

    public void SetTarget(IUnit unit) => Target = unit;
    public void SetStats(EnemyData stats) => Stats = stats;

    
    public override string ToString() => $"Unit({transform.name})";
  }
}