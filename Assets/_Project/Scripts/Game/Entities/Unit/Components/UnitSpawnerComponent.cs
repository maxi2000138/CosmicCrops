using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Entities.Unit.Components
{
  public class UnitSpawnerComponent : MonoComponent<UnitSpawnerComponent>
  { 
    public Vector3 Position => transform.position;
  }
}