using _Project.Scripts.Game.Units.Loot.Interface;

namespace _Project.Scripts.Game.Collector.Interfaces
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