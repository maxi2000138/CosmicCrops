using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Features.Units.Enemy.Actions
{
  public class ScoredAction : UnitAction
  {
    public float Score;
    
    public ScoredAction(BattleAction battleAction, float score)
    {
      Score = score;
      ActionType = battleAction.ActionType;
      Unit = battleAction.Unit;
    }
    
    public override string ToString()
    {
      string actionCategory = "other";

      switch(ActionType)
      {
        case UnitActionType.Patrol:
          actionCategory = "patrol";
          break;
        case UnitActionType.Pursuit:
          actionCategory = "pursuit";
          break;
        case UnitActionType.Fight:
          actionCategory = "fight";
          break;
      }
      
      return $"{actionCategory}: {ActionType} target: {Unit} score: {Score}";
    }
  }
}