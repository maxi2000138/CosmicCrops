using R3;

namespace _Project.Scripts.Infrastructure.Systems.Components
{
  public abstract class Component : IComponent
  {
    public CompositeDisposable LifetimeDisposable { get; private set; }

    protected Component()
    {
      OnComponentCreate();
      OnComponentEnable();
    }

    ~Component()
    {
      OnComponentDisable();
      OnComponentDestroy();
    }

    public virtual void OnComponentCreate() => LifetimeDisposable = new CompositeDisposable();
    public virtual void OnComponentEnable() { }
    public virtual void OnComponentDisable() => LifetimeDisposable?.Clear();
    public virtual void OnComponentDestroy() => LifetimeDisposable?.Dispose();
  }
}