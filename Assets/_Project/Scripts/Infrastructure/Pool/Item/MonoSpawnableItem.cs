using UnityEngine;

namespace _Project.Scripts.Infrastructure.Pool.Item
{
  public class MonoSpawnableItem : MonoBehaviour, ISpawnableItem
  {
    private IObjectPoolService _objectPool;
    
    public void SetParent(Transform root)
    {
      transform.SetParent(root);
    }

    public virtual void Remove()
    {
      if (_objectPool is null)
      {
        OnRemoved();
        return;
      }
            
      _objectPool.ReleaseObject(this);
    }

    
    public virtual void OnCreated(IObjectPoolService objectPool)
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