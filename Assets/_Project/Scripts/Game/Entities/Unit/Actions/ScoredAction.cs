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
  }
}