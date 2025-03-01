using R3;

namespace _Project.Scripts.Infrastructure.Systems.Components
{ 
  public interface IComponent
  {
    CompositeDisposable LifetimeDisposable { get; }

    void OnComponentCreate();
    void OnComponentEnable();
    void OnComponentDisable();
    void OnComponentDestroy();
  }
}