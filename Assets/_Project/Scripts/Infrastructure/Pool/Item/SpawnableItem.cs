namespace _Project.Scripts.Infrastructure.Pool.Item
{
  public abstract class SpawnableItem : ISpawnableItem
  {
    private IObjectPool _objectPool;

    public void Remove()
    {
      if (_objectPool is null)
      {
        OnRemoved();
        return;
      }
            
      _objectPool.Despawn(this);
    }

    public virtual void OnCreated(IObjectPool objectPool)
    {
      _objectPool = objectPool;
    }
    public virtual void OnSpawned() { }
    public virtual void OnDespawned() { }
    public virtual void OnRemoved() { }
  }
}