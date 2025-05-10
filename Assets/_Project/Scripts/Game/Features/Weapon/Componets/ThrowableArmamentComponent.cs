using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Componets
{
  public class ThrowableArmamentComponent : MonoComponent<ThrowableArmamentComponent>
  {
    [SerializeField] private ArmamentComponent _armament;
    [SerializeField] private AnimationCurve _trajectory;
    [SerializeField] private float _throwHeight;
    
    public ArmamentComponent Armament => _armament;
    public float ThrowHeight => _throwHeight;
    public float InitialDistance { get; private set; }
    public float InitialHeight { get; private set; }
    
    public void SetInitialSqrDistance(float distance) => InitialDistance = distance;
    public void SetInitialHeight(float height) => InitialHeight = height;
    public AnimationCurve Trajectory => _trajectory;

  }
}