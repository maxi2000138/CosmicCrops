using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.UtilityAI
{
  public interface IUtilityFunction
  {
    string Name { get; }
    float GetInput(BattleAction battleAction, EnemyComponent enemy);
    float Score(float score, EnemyComponent enemy);
    bool AppliesTo(BattleAction battleAction, EnemyComponent enemy);
  }
}