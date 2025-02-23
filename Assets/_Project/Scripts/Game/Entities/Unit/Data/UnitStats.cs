using UnityEngine;

namespace _Project.Scripts.Game.Entities.Unit.Data
{
  [System.Serializable]
  public struct UnitStats
  {
    [Range(1, 500)] public int Health;
  }
}