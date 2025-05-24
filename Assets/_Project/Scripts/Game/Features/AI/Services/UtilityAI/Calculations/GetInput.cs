using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;
using UnityEngine;

namespace _Project.Scripts.Game.Features.AI.UtilityAI.Calculations
{
  public static class GetInput
  {
    public static float DistanceToTarget(BattleAction battleAction, UnitComponent unit)
    {
      return Vector3.Distance(unit.Position, battleAction.Producer.Position);
    }
  }
}