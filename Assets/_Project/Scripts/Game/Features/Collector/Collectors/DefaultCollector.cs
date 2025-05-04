using System.Linq;
using _Project.Scripts.Game.Features.Collector._Configs;
using _Project.Scripts.Game.Features.Collector.Interfaces;
using _Project.Scripts.Game.Features.Inventory;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Loot.Interface;
using _Project.Scripts.Infrastructure.Logger;
using _Project.Scripts.Infrastructure.Time;
using R3;

namespace _Project.Scripts.Game.Features.Collector.Collectors
{
  public class DefaultCollector : ICollector
  {
    private readonly InventoryModel _inventoryModel;
    private readonly LevelModel _levelModel;
    private readonly ITimeService _time;
    private readonly CollectorsConfig _collectorsConfig;

    private bool _collecting;
    private float _collectingTimeLeft;
    
    private ILoot _loot;

    public CollectorType CollectorType => CollectorType.Default;
    public float CollectorDistance => CollectorData.CollectRadius;
    
    private CollectorData CollectorData => _collectorsConfig.Data[CollectorType];

    public DefaultCollector(InventoryModel inventoryModel, LevelModel levelModel, ITimeService time, CollectorsConfig collectorsConfig)
    {
      _inventoryModel = inventoryModel;
      _levelModel = levelModel;
      _time = time;
      _collectorsConfig = collectorsConfig;
    }
      
    public void Initialize()
    {
      ReadyCollect();
    }
    
    public bool CanCollect() => _collecting == false;
    public bool IsCollecting() => _collecting;

    public void StartCollecting(ILoot loot)
    {
      _loot = loot;
      DebugLogger.Log("Loot Collecting...", LogsType.Collector);

      NotReadyCollect();

      ReloadCollectingTime(CollectorData.CollectTime);
      
      _inventoryModel.StartCollectingLoot.Execute(CollectorData.CollectTime);
    }

    public void CancelCollecting()
    {
      if(_loot == null)
        return;

      ReadyCollect();
      ReloadCollectingTime(0f);

      DebugLogger.Log("Cancel Collect :(", LogsType.Collector);

      _inventoryModel.CancelCollectingLoot.Execute(Unit.Default);
    }

    public void Execute()
    {
      if (Collecting())
      {
        _collectingTimeLeft -= _time.DeltaTime;
        
        if (_collectingTimeLeft <= 0)
        {
          CompleteCollecting();
          DebugLogger.Log("Loot Collected!", LogsType.Collector);
          ReadyCollect();
        }
      }
    }
    
    private void CompleteCollecting()
    {
      if (_loot == null)
        return;
      
      _levelModel.RemoveLoot(_loot);
      
      _loot.Remove();
      _loot = null;
    }
    
    private bool Collecting() => _collecting;
    private void ReadyCollect() => _collecting = false;
    private void NotReadyCollect() => _collecting = true;
    private void ReloadCollectingTime(float time) => _collectingTimeLeft = time;
  }
}