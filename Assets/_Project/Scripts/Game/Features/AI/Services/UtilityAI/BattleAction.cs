using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;

namespace _Project.Scripts.Game.Features.AI.Services.UtilityAI
{
  public class BattleAction
  {
    public ITarget Producer;
    public ITarget Target;
    public UnitActionType ActionType;
  }
}