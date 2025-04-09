using CodeBase.Infrastructure.Pool;

namespace _Project.Scripts.Infrastructure.Pool.Item
{
  public class SpawnableItem : ISpawnableItem
  {
    public virtual void Remove()
    {
      OnRemoved();
    }
    
    public virtual void OnCreated(IObjectPoolService objectPool) { }
    public virtual void OnSpawned() { }
    public virtual void OnDespawned() { }
    public virtual void OnRemoved() { }
  }
}