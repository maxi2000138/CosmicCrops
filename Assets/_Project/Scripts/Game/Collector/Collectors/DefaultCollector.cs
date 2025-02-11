using _Project.Scripts.Game.Collector.Interfaces;
using _Project.Scripts.Game.Inventory;
using _Project.Scripts.Game.Level.Model;
using _Project.Scripts.Game.Units.Loot.Interface;
using _Project.Scripts.Infrastructure.Logger;
using _Project.Scripts.Infrastructure.Time;
using R3;

namespace _Project.Scripts.Game.Collector.Collectors
{
  public class DefaultCollector : ICollector
  {
    private readonly InventoryModel _inventoryModel;
    private readonly LevelModel _levelModel;
    private readonly ITimeService _time;

    private bool _collecting;
    private float _collectingTimeLeft;
    
    private ILoot _loot;

    public DefaultCollector(InventoryModel inventoryModel, LevelModel levelModel, ITimeService time)
    {
      _inventoryModel = inventoryModel;
      _levelModel = levelModel;
      _time = time;
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

      float collectTime = 5f;
      ReloadCollectingTime(collectTime);
      
      _inventoryModel.StartCollectingLoot.Execute(collectTime);
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
      
      _loot.Destroy();
      _loot = null;
    }
    
    private bool Collecting() => _collecting;
    private void ReadyCollect() => _collecting = false;
    private void NotReadyCollect() => _collecting = true;
    private void ReloadCollectingTime(float time) => _collectingTimeLeft = time;
  }
}