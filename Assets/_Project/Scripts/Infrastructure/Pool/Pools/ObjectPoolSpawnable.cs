using System;
using _Project.Scripts.Infrastructure.Pool.Item;

namespace _Project.Scripts.Infrastructure.Pool
{
  public class ObjectPoolSpawnable<T> : ObjectPool<T> where T : class, ISpawnableItem
  {
    protected ObjectPoolSpawnable()
    {
            
    }

    protected ObjectPoolSpawnable(Func<T> spawner) : base(spawner)
    {
      
    }

    protected ObjectPoolSpawnable(Func<T> spawner, int initSize) : base(spawner, initSize)
    {
      Resize(initSize);
    }

    protected override void OnItemCreated(T item)
    {
      item.OnCreated(this);
    }

    protected override void OnItemSpawned(T item)
    {
      base.OnItemSpawned(item);
      item.OnSpawned();
    }
    
    protected override void OnDespawnItem(T item)
    {
      base.OnDespawnItem(item);
      item.OnDespawned();
    }

    protected override void OnRemoveItem(T item)
    {
      base.OnRemoveItem(item);
      item.OnRemoved();
    }
  }
}