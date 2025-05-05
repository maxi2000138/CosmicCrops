using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.LifeTime.EntryPoints.Core
{
  public class EntryPointBase : IEntryPoint
  {
    protected virtual void Entry() { }

    void IStartable.Start()
    {
      Entry();
    }

    
    public virtual void Dispose()
    {
      
    }
  }
}