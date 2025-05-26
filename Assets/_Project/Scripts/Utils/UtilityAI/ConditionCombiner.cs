using System;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Utils.UtilityAI
{
  public static class UtilityAIExtensions
  {
    public static Func<BattleAction, UnitComponent, bool> And(
      this Func<BattleAction, UnitComponent, bool> first,
      Func<BattleAction, UnitComponent, bool> second)
    {
      return (action, unit) => first(action, unit) && second(action, unit);
    }

    public static Func<BattleAction, UnitComponent, bool> Or(
      this Func<BattleAction, UnitComponent, bool> first,
      Func<BattleAction, UnitComponent, bool> second)
    {
      return (action, unit) => first(action, unit) || second(action, unit);
    }
  }
}