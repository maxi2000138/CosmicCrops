using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Collector.Components;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Entities.Character.Components
{
  public class CharacterComponent : MonoComponent<CharacterComponent>, ICharacter
  {
    [SerializeField] private UnitAnimatorComponent _unitAnimator;
    [SerializeField] private CharacterControllerComponent _characterController;
    [SerializeField] private CollectorComponent _collector;
    [SerializeField] private StateMachineComponent _stateMachine;
    [SerializeField] private WeaponComponent _weaponComponent;
    [SerializeField] private HealthComponent _healthComponent;


    public UnitAnimatorComponent UnitAnimator => _unitAnimator;
    public CharacterControllerComponent CharacterController => _characterController;
    public CollectorComponent Collector => _collector;
    public StateMachineComponent StateMachine => _stateMachine;
    public WeaponComponent WeaponComponent => _weaponComponent;
    public HealthComponent Health => _healthComponent;

    public Vector3 Position => transform.position;
    public Vector3 Forward => transform.forward;

    //TODO setup from config
    public float Height => 3f;
  }
}