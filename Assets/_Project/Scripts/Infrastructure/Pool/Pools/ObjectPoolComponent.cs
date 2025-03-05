using System;
using _Project.Scripts.Infrastructure.Pool.Item;
using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Infrastructure.Pool
{
  public class ObjectPoolComponent<T> : ObjectPoolSpawnable<T> where T : Component<T>, ISpawnableItem
  {

    public ObjectPoolComponent(Func<T> spawner, int initSize) : base(spawner, initSize) { }
    
    protected override void OnItemSpawned(T item)
    {
      base.OnItemSpawned(item);
      
      ComponentsContainer<T>.Registered(item);
    }
    
    protected override void OnDespawnItem(T item)
    {
      base.OnDespawnItem(item);

      ComponentsContainer<T>.Unregistered(item);
    }
  }
}