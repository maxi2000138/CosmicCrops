using _Project.Scripts.Infrastructure.Services;
using _Project.Scripts.Infrastructure.Services.Logger;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.Scopes
{
  public class MenuScope : LifetimeScope
  {
    public void SetupAndBuild()
    {
      Build();
      
      DebugLogger.Log("Loaded menu", LogsType.Infrastructure);
    }
    
    protected override void Configure(IContainerBuilder builder)
    {
      base.Configure(builder);
      
      
    }
  }
}