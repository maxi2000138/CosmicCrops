using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Entities.Enemy.Actions
{
  public class UnitAction
  {
    public UnitActionType ActionType;
    public TargetType TargetType;
    public IUnit Unit;
  }
}