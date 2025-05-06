using System.Collections;
using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Utils.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Game.Entities.Unit.Components
{
  public class UnitSpawnerComponent : MonoComponent<UnitSpawnerComponent>
  { 
    [ValueDropdown(nameof(GetAllUnits))]
    [SerializeField] private string _unit;

    public Vector3 Position => transform.position;
    public string Unit => _unit;

    
    private static IEnumerable GetAllUnits()
    {
      return ConfigsUtils.GetAllUnitNames();
    }
  }
}