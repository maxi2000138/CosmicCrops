using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Components
{
  public class DirectionArmamentComponent : MonoComponent<DirectionArmamentComponent>
  {
    [SerializeField] private ArmamentComponent _armament;
    
    public ArmamentComponent Armament => _armament;
    public Vector3 TargetDirection { get; private set; }

    public void SetTargetDirection(Vector3 direction) => TargetDirection = direction;
  }
}