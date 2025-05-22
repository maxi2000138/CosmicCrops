using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.UtilityAI
{
  public interface IUtilityFunction
  {
    string Name { get; }
    float GetInput(BattleAction battleAction, IEnemy enemy);
    float Score(float score, IEnemy enemy);
    bool AppliesTo(BattleAction battleAction, IEnemy enemy);
  }
}