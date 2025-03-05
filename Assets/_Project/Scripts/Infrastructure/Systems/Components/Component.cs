using _Project.Scripts.Infrastructure.Pool;
using _Project.Scripts.Infrastructure.Pool.Item;
using R3;

namespace _Project.Scripts.Infrastructure.Systems.Components
{
  public abstract class Component<T> : SpawnableItem, IComponent where T : IComponent
  {
    public CompositeDisposable LifetimeDisposable { get; private set; }

    public override void OnCreated(IObjectPool objectPool)
    {
      base.OnCreated(objectPool);
      
      OnComponentCreate();
    }
    
    public override void OnSpawned()
    {
      base.OnSpawned();
      
      OnComponentEnable();
    }
    
    public override void OnDespawned()
    {
      base.OnDespawned();

      OnComponentDisable();
    }
    
    public override void OnRemoved()
    {
      base.OnRemoved();

      OnComponentDestroy();
    }
    
    public virtual void OnComponentCreate() => LifetimeDisposable = new CompositeDisposable();
    public virtual void OnComponentEnable() { }
    public virtual void OnComponentDisable() => LifetimeDisposable?.Clear();
    public virtual void OnComponentDestroy() => LifetimeDisposable?.Dispose();
  }
}