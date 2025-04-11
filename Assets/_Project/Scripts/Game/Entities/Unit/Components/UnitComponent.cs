using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Game.Entities.Unit._Configs;
using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Weapon.Componets;
using UnityEngine;

namespace _Project.Scripts.Game.Entities.Unit.Components
{
  public class UnitComponent : MonoComponent<UnitComponent>, IEnemy 
  {
    [SerializeField] private AgentComponent _agent;
    [SerializeField] private StateMachineComponent _stateMachine;
    [SerializeField] private UnitAnimatorComponent animator;
    [SerializeField] private HealthComponent _health;
    [SerializeField] private WeaponMediatorComponent _weaponMediatorComponent;


    public UnitData Stats { get; set; }

    public AgentComponent Agent => _agent;
    public UnitAnimatorComponent Animator => animator;
    public WeaponMediatorComponent WeaponMediator => _weaponMediatorComponent;
    public StateMachineComponent StateMachine => _stateMachine;
    public HealthComponent Health => _health;
    public Vector3 Position => transform.position;
    public float Height => Stats.Height;
    public Vector3 Forward => transform.forward;
  }
}