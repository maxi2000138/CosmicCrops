using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.UtilityAI.Calculations
{
  public static class GetInput
  {
    public static float DistanceToTarget(BattleAction battleAction, IEnemy target)
    {
      return target.Position - battleAction.Producer.Position;
    }
  }
}