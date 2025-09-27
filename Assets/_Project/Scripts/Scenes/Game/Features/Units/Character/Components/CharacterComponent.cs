using _Project.Scripts.Game.Features.Collector.Components;
using _Project.Scripts.Game.Features.Units._Components;
using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Game.UI.Radar.Components;
using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Scenes.Game.Features.Units._Components;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Units.Character.Components
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
    [SerializeField] private BodyMediatorComponent _bodyMediatorComponent;
    


    public IUnit Target { get; private set; }
    public float Height { get; private set; }
    public UnitAnimatorComponent UnitAnimator => _unitAnimator;
    public CharacterControllerComponent CharacterController => _characterController;
    public CollectorComponent Collector => _collector;
    public StateMachineComponent StateMachine => _stateMachine;
    public WeaponMediatorComponent WeaponMediator => _weaponMediatorComponent;
    public HealthComponent Health => _healthComponent;
    public RadarComponent Radar => _radarComponent;
    public BodyMediatorComponent BodyMediator => _bodyMediatorComponent;

    public Vector3 Position => transform.position;
    public Vector3 Forward => transform.forward;


    public void SetHeight(float height) { Height = height; }
    public void SetTarget(IUnit unit) { Target = unit; }

    public override string ToString() => "Character";
  }
}