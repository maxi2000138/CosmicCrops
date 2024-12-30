using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Factories.StateMachine;
using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.Services;
using _Project.Scripts.Infrastructure.Services.Logger;
using _Project.Scripts.Infrastructure.StateMachine.States;
using _Project.Scripts.Infrastructure.StaticData;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.Scopes
{
  public class BootstrapScope : LifetimeScope
  {
    private string _startScene;

    protected override void Awake()
    {
      base.Awake();
      DontDestroyOnLoad(this);
    }

    public void SetupAndBuild(string scene)
    {
      _startScene = scene;
      Build();
    }
    
    protected override void Configure(IContainerBuilder builder)
    {
      base.Configure(builder);

      builder.Register<DebugLogger>(Lifetime.Singleton);
      builder.Register<IAssetService, AssetService>(Lifetime.Singleton);
      builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
      builder.Register<IStateMachineFactory, StateMachineFactory>(Lifetime.Singleton);
      builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Singleton);

      builder.RegisterBuildCallback(ResolveNonLaziesServices);
      builder.RegisterBuildCallback(EnterGameStateMachine);
    }
    
    private void EnterGameStateMachine(IObjectResolver container)
    {
      container.Resolve<IStateMachineFactory>().CreateGameStateMachine(_startScene).Enter<StateBootstrap>();
    }

    private void ResolveNonLaziesServices(IObjectResolver container)
    {
      container.Resolve<DebugLogger>();
    }
  }
}