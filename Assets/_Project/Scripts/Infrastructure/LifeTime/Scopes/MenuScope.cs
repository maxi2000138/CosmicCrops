using _Project.Scripts.Infrastructure.LifeTime.EntryPoints;
using _Project.Scripts.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine.States;
using VContainer;
using VContainer.Unity;
using IState = _Project.Scripts.Infrastructure.StateMachine.States.Interfaces.IState;

namespace _Project.Scripts.Infrastructure.LifeTime.Scopes
{
  public class MenuScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      base.Configure(builder);

      builder.RegisterEntryPoint<MenuEntryPoint>();

      builder.Register<IState, StateMenuBootstrap>(Lifetime.Singleton);
    }
  }
}