using System.Collections.Generic;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Features.AI.UtilityAI;
using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Game.Entities.Unit.Components
{
  public class EnemyAIComponent : MonoComponent<EnemyAIComponent>
  {
    public IEnumerable<UnitAction> Actions =>
      new List<UnitAction>
      {
        new UnitAction { ActionType = UnitActionType.Patrol   , TargetType = TargetType.Self },
        new UnitAction { ActionType = UnitActionType.Pursuit  , TargetType = TargetType.EnemyOrCharacter },
        new UnitAction { ActionType = UnitActionType.Fight    , TargetType = TargetType.EnemyOrCharacter },
      };
  }
}