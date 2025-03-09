using _Project.Scripts.Infrastructure.Pool.Item;
using R3;

namespace _Project.Scripts.Infrastructure.Systems.Components
{
  public abstract class MonoComponent : MonoSpawnableItem, IComponent
  {
    public CompositeDisposable LifetimeDisposable { get; private set; }

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);

    
    public virtual void OnComponentCreate() => LifetimeDisposable = new CompositeDisposable();
    public virtual void OnComponentEnable() { }
    public virtual void OnComponentDisable() => LifetimeDisposable?.Clear();
    public virtual void OnComponentDestroy() => LifetimeDisposable?.Dispose();
    

    private void Awake()
    {
      OnComponentCreate();
    }

    private void OnDestroy()
    {
      OnComponentDestroy();
    }
  }
  
  public abstract class MonoComponent<T> : MonoComponent where T : MonoComponent
  {
    private void OnEnable()
    {
      base.OnComponentEnable();
      ComponentsContainer<T>.Registered(this);
    }

    private void OnDisable()
    {
      base.OnComponentDisable();
      ComponentsContainer<T>.Unregistered(this);
    }
  }
}