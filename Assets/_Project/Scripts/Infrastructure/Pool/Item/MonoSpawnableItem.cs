using UnityEngine;

namespace _Project.Scripts.Infrastructure.Pool.Item
{
  public class MonoSpawnableItem : MonoBehaviour, ISpawnableItem
  {
    private IObjectPool _objectPool;
    
    public virtual void Remove()
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
    public virtual void OnRemoved()
    {
      Destroy(gameObject);
    }

  }
}