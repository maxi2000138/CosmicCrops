using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.UtilityAI.Calculations
{
  public static class When
  {

    public static bool ActionIsPursuit(BattleAction battleAction, ITarget enemy)
    {
      return battleAction.ActionType == UnitActionType.Pursuit;
    }
    
    public static bool ActionIsFight(BattleAction arg1, IEnemy arg2)
    {
      return arg1.ActionType == UnitActionType.Fight;
    }
  }

}