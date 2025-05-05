using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.LifeTime.EntryPoints.Core
{
  public class EntryPointBase : IEntryPoint
  {
    public virtual void Initialize() { }
    public virtual void Entry() { }

    void IStartable.Start()
    {
      Entry();
    }

    
    public virtual void Dispose()
    {
      
    }
  }
}