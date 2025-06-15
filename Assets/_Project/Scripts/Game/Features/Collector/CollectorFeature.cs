using _Project.Scripts.Game.Features.Collector.Systems;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.Features.Collector
{
  public class CollectorFeature : Feature
  {
    public CollectorFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new ExecuteCollectorSystem());
      
      Add(new CollectingViewSystem());
    }
  }
}