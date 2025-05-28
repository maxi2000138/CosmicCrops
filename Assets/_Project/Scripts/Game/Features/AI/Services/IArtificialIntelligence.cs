using _Project.Scripts.Game.Entities.Enemy.Actions;
using _Project.Scripts.Game.Entities.Enemy.Components;

namespace _Project.Scripts.Game.Features.AI.Services
{
  public interface IArtificialIntelligence
  {
    UnitAction MakeBestDecision(EnemyComponent enemy);
  }
}