using _Project.Scripts._Infrastructure.LifeTime.Systems;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._Infrastructure.LifeTime.Scopes
{
  public class GameScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      base.Configure(builder);
      
      builder.RegisterEntryPoint<EntryPointGameSystem>(Lifetime.Scoped).AsSelf().Build();
    }
  }
}