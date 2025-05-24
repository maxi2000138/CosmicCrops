using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.UtilityAI.Calculations
{
  public static class When
  {

    public static bool ActionIsPursuit(BattleAction battleAction, UnitComponent unit)
    {
      return battleAction.ActionType == UnitActionType.Pursuit;
    }
    
    public static bool ActionIsFight(BattleAction arg1, UnitComponent unit)
    {
      return arg1.ActionType == UnitActionType.Fight;
    }
  }

}