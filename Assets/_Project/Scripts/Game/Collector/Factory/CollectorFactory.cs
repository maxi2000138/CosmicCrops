using _Project.Scripts.Game.Collector.Collectors;
using _Project.Scripts.Game.Collector.Interfaces;
using _Project.Scripts.Game.Inventory;
using _Project.Scripts.Game.Level.Model;
using _Project.Scripts.Infrastructure.Time;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Game.Collector.Factory
{

  public class CollectorFactory : ICollectorFactory
  {
    private readonly IObjectResolver _objectResolver;
    public CollectorFactory(IObjectResolver objectResolver)
    {
      _objectResolver = objectResolver;
    }
    
    public ICollector CreateDefault()
    {
      var collector = new DefaultCollector(_objectResolver.Resolve<InventoryModel>(), _objectResolver.Resolve<LevelModel>(), _objectResolver.Resolve<ITimeService>());
      collector.Initialize();
      
      return collector;
    }
  }
}