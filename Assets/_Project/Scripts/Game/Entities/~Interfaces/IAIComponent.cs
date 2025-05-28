using _Project.Scripts.Game.Entities.Unit.Components;

namespace _Project.Scripts.Game.Entities._Interfaces
{
  public interface IAIComponent
  {
    EnemyAIComponent AI { get; }
  }
}