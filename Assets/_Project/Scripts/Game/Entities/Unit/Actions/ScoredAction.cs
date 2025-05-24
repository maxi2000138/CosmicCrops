using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Entities.Unit.Actions
{
  public class ScoredAction : UnitAction
  {
    public float Score;
    
    public ScoredAction(BattleAction battleAction, float score)
    {
      Score = score;
      ActionType = battleAction.ActionType;
      Target = battleAction.Target;
    }
    
    public override string ToString()
    {
      string actionCategory = "other";

      switch(ActionType)
      {
        case UnitActionType.Idle:
          actionCategory = "idle";
          break;
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
      
      return $"{actionCategory}: {ActionType} target: {Target} score: {Score}";
    }
  }
}