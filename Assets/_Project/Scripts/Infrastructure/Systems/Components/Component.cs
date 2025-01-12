using R3;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Systems.Components
{
  public abstract class Component : MonoBehaviour
  {
    public CompositeDisposable LifetimeDisposable { get; private set; }

    protected virtual void OnComponentCreate() => LifetimeDisposable = new CompositeDisposable();
    protected virtual void OnComponentEnable() { }
    protected virtual void OnComponentDisable() => LifetimeDisposable?.Clear();
    protected virtual void OnComponentDestroy() => LifetimeDisposable?.Dispose();
  }
}