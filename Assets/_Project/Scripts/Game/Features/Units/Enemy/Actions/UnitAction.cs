using _Project.Scripts.Game.Features.AI.Services.UtilityAI;
using _Project.Scripts.Game.Features.Units._Interfaces;

namespace _Project.Scripts.Game.Features.Units.Enemy.Actions
{
  public class UnitAction
  {
    public UnitActionType ActionType;
    public TargetType TargetType;
    public IUnit Unit;
  }
}