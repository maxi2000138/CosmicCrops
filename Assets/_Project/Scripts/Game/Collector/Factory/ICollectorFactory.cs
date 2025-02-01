using _Project.Scripts.Game.Collector.Interfaces;

namespace _Project.Scripts.Game.Collector.Factory
{
  public interface ICollectorFactory
  {
    ICollector CreateDefault();
  }
}