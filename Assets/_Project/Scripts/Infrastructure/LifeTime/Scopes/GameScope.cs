using _Project.Scripts.Infrastructure.LifeTime.EntryPoints;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.LifeTime.Scopes
{
  public class GameScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      base.Configure(builder);
      
      builder.RegisterEntryPoint<EntryPointGameSystem>().AsSelf().Build();
    }
  }
}