using _Project.Scripts.Game.Entities.Enemy.Components;

namespace _Project.Scripts.Game.Features.AI.Services.UtilityAI
{
  public interface IUtilityFunction
  {
    string Name { get; }
    float GetInput(BattleAction battleAction, EnemyComponent enemy);
    float Score(float score, EnemyComponent enemy);
    bool AppliesTo(BattleAction battleAction, EnemyComponent enemy);
  }
}