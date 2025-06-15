using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Components
{
  public class HomingArmamentComponent : MonoComponent<HomingArmamentComponent>
  {
    [SerializeField] private ArmamentComponent _armament;
    
    public ArmamentComponent Armament => _armament;
    public IUnit Unit { get; private set; }

    public void SetTarget(IUnit unit) => Unit = unit;
  }
}