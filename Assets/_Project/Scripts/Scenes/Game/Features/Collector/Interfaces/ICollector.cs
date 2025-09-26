using _Project.Scripts.Game.Features.Collector._Configs;
using _Project.Scripts.Game.Features.Collector._Configs.Data;
using _Project.Scripts.Game.Features.Loot.Interface;

namespace _Project.Scripts.Game.Features.Collector.Interfaces
{
  public interface ICollector
  {
    CollectorType CollectorType { get; }
    public float CollectorDistance { get;  }

    void StartCollecting(ILoot loot);
    bool CanCollect();
    bool IsCollecting();
    void Execute();
    void CancelCollecting();
  }
}