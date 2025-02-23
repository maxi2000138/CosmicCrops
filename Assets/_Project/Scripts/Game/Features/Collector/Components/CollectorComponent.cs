using _Project.Scripts.Game.Features.Collector.Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Game.Features.Collector.Components
{
  public class CollectorComponent : MonoComponent<CollectorComponent>
  {
    public ICollector Collector { get; private set; }
    
    public void SetCollector(ICollector collector)
    {
      Collector = collector;
    }
  }
}