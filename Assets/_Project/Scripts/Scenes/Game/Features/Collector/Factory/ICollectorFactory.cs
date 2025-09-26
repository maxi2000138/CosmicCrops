using _Project.Scripts.Game.Features.Collector.Interfaces;

namespace _Project.Scripts.Game.Features.Collector.Factory
{
  public interface ICollectorFactory
  {
    ICollector CreateDefault();
  }
}