using _Project.Scripts.Game.Entities.Unit.Data;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Entities.Unit.Components
{
  public class UnitSpawnerComponent : MonoComponent<UnitSpawnerComponent>
  { 
    [SerializeField] private UnitStats _unitStats;

    public UnitStats UnitStats => _unitStats;
    public Vector3 Position => transform.position;
  }
}