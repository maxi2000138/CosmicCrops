using _Project.Scripts.Game.Features.Units.Enemy.Actions;
using _Project.Scripts.Game.Features.Units.Enemy.Components;

namespace _Project.Scripts.Game.Features.AI.Services
{
  public interface IArtificialIntelligence
  {
    UnitAction MakeBestDecision(EnemyComponent enemy);
  }
}