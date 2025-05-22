using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.AI.UtilityAI;

namespace _Project.Scripts.Game.Entities.Unit.Actions
{
  public class UnitAction
  {
    public UnitActionType ActionType;
    public TargetType TargetType;
    public ITarget Target;
  }
}