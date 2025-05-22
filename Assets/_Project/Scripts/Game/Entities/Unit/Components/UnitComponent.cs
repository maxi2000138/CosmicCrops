using System.Collections.Generic;
using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Game.Entities.Unit._Configs;
using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Features.AI.UtilityAI;
using _Project.Scripts.Game.Features.UtilityAI;
using _Project.Scripts.Game.Features.Weapon.Componets;
using UnityEngine;

namespace _Project.Scripts.Game.Entities.Unit.Components
{
  public class UnitComponent : MonoComponent<UnitComponent>, IEnemy, IAiTarget
  {
    [SerializeField] private AgentComponent _agent;
    [SerializeField] private StateMachineComponent _stateMachine;
    [SerializeField] private UnitAnimatorComponent animator;
    [SerializeField] private HealthComponent _health;
    [SerializeField] private WeaponMediatorComponent _weaponMediatorComponent;

    public UnitData Stats { get; private set; }
    public ITarget Target { get; private set; }

    public AgentComponent Agent => _agent;
    public UnitAnimatorComponent Animator => animator;
    public WeaponMediatorComponent WeaponMediator => _weaponMediatorComponent;
    public StateMachineComponent StateMachine => _stateMachine;
    public HealthComponent Health => _health;
    public Vector3 Position => transform.position;
    public float Height => Stats.Height;
    public Vector3 Forward => transform.forward;

    public void SetTarget(ITarget target) => Target = target;
    public void SetStats(UnitData stats) => Stats = stats;

    public IEnumerable<UnitAction> Actions =>
      new List<UnitAction>
      {
        new UnitAction { ActionType = UnitActionType.Idle     , TargetType = TargetType.Self },
        new UnitAction { ActionType = UnitActionType.Patrol   , TargetType = TargetType.Self },
        new UnitAction { ActionType = UnitActionType.Pursuit  , TargetType = TargetType.EnemyOrCharacter },
        new UnitAction { ActionType = UnitActionType.Fight    , TargetType = TargetType.EnemyOrCharacter },
      };
  }
}