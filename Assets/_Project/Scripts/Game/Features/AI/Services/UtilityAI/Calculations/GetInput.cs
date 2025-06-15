using _Project.Scripts.Game.Features.Units.Enemy.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Features.AI.Services.UtilityAI.Calculations
{
  public class GetInput
  {
    public float TargetPercentageHealth(BattleAction battleAction, EnemyComponent enemy)
    {
      return (float)battleAction.Unit.Health.CurrentHealth.CurrentValue / battleAction.Unit.Health.MaxHealth;
    }
    
    public float DistanceToTarget(BattleAction battleAction, EnemyComponent enemy)
    {
      return Vector3.Distance(enemy.Position, battleAction.Unit.Position);
    }
    
    public float One(BattleAction battleAction, EnemyComponent enemy)
    {
      return 1f;
    }
  }
}