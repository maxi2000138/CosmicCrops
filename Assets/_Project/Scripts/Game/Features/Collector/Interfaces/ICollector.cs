using _Project.Scripts.Game.Features.Loot.Interface;

namespace _Project.Scripts.Game.Features.Collector.Interfaces
{
  public interface ICollector
  {
    void StartCollecting(ILoot loot);
    bool CanCollect();
    bool IsCollecting();
    void Execute();
    void CancelCollecting();
  }
}