using _Project.Scripts._Infrastructure.Services.Logger;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._Infrastructure.Scopes
{
  public class GameScope : LifetimeScope
  {
    public void SetupAndBuild(int level)
    {
      Build();
      
      DebugLogger.Log("Loaded level: " + level, LogsType.Infrastructure);
    }

    protected override void Configure(IContainerBuilder builder)
    {
      base.Configure(builder);
    }
  }
}