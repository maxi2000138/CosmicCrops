using System;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;
using _Project.Scripts.Game.Features.Units.Enemy.Components;

namespace _Project.Scripts.Utils.UtilityAI
{
  public static class ConditionCombiner
  {
    public static Func<BattleAction, EnemyComponent, bool> AllOf(
      params Func<BattleAction, EnemyComponent, bool>[] conditions)
    {
      return (action, unit) =>
      {
        foreach (var cond in conditions)
        {
          if (!cond(action, unit))
            return false;
        }
        return true;
      };
    }
    
    public static Func<BattleAction, EnemyComponent, bool> And(
      Func<BattleAction, EnemyComponent, bool> a,
      Func<BattleAction, EnemyComponent, bool> b)
    {
      return (action, unit) => a(action, unit) && b(action, unit);
    }

    public static Func<BattleAction, EnemyComponent, bool> Or(
      Func<BattleAction, EnemyComponent, bool> a,
      Func<BattleAction, EnemyComponent, bool> b)
    {
      return (action, unit) => a(action, unit) || b(action, unit);
    }

    public static Func<BattleAction, EnemyComponent, bool> Not(
      Func<BattleAction, EnemyComponent, bool> a)
    {
      return (action, unit) => !a(action, unit);
    }
  }
}