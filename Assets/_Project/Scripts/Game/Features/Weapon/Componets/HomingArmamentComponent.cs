using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Componets
{
  public class HomingArmamentComponent : MonoComponent<HomingArmamentComponent>
  {
    [SerializeField] private ArmamentComponent _armament;
    
    public ArmamentComponent Armament => _armament;
    public ITarget Target { get; private set; }

    public void SetTarget(ITarget target) => Target = target;
  }
}