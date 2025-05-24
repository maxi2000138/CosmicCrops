using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Collector.Components;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Game.Entities.Character.Components
{
  public class CharacterComponent : MonoComponent<CharacterComponent>, ICharacter
  {
    [SerializeField] private UnitAnimatorComponent _unitAnimator;
    [SerializeField] private CharacterControllerComponent _characterController;
    [SerializeField] private CollectorComponent _collector;
    [SerializeField] private StateMachineComponent _stateMachine;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private WeaponMediatorComponent _weaponMediatorComponent;
    [SerializeField] private RadarComponent _radarComponent;


    public UnitAnimatorComponent UnitAnimator => _unitAnimator;
    public CharacterControllerComponent CharacterController => _characterController;
    public CollectorComponent Collector => _collector;
    public StateMachineComponent StateMachine => _stateMachine;
    public WeaponMediatorComponent WeaponMediator => _weaponMediatorComponent;
    public HealthComponent Health => _healthComponent;
    public RadarComponent Radar => _radarComponent;

    public Vector3 Position => transform.position;
    public Vector3 Forward => transform.forward;

    public float Height { get; private set; }

    public void SetHeight(float height)
    {
      Height = height;
    }

    public override string ToString() => "Character";
  }
}