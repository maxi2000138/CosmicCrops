using System;
using _Project.Scripts.Game.Infrastructure.Systems;
using _Project.Scripts.Infrastructure.LifeTime.EntryPoints;
using _Project.Scripts.Infrastructure.StateMachine.States;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Menu.Features;
using _Project.Scripts.Menu.Features.CharacterPreview.Model;
using _Project.Scripts.Menu.Features.RenderTexture.Factory;
using _Project.Scripts.Menu.Infrastructure.Factory;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.LifeTime.Scopes
{
  public class MenuScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      base.Configure(builder);

      builder.RegisterEntryPoint<MenuEntryPoint>();
      builder.Register<Feature, MenuFeature>(Lifetime.Singleton);
      builder.Register<SystemsContainer>(Lifetime.Singleton).AsImplementedInterfaces();

      builder.Register<CharacterPreviewModel>(Lifetime.Singleton).As<IInitializable, IDisposable>().AsSelf();

      builder.Register<IMenuFactory, MenuFactory>(Lifetime.Singleton);
      builder.Register<IRenderTextureFactory, RenderTextureFactory>(Lifetime.Singleton);
      
      builder.Register<IState, StateMenuBootstrap>(Lifetime.Singleton);
    }
  }
}