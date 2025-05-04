using _Project.Scripts.Game.Features.Collector._Configs;
using _Project.Scripts.Game.Features.Collector.Collectors;
using _Project.Scripts.Game.Features.Collector.Interfaces;
using _Project.Scripts.Game.Features.Inventory;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Infrastructure.Time;
using VContainer;

namespace _Project.Scripts.Game.Features.Collector.Factory
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
      var collector = new DefaultCollector(_objectResolver.Resolve<InventoryModel>(), _objectResolver.Resolve<LevelModel>(), 
        _objectResolver.Resolve<ITimeService>(), _objectResolver.Resolve<CollectorsConfig>());
      
      collector.Initialize();
      
      return collector;
    }
  }
}