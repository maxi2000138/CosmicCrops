using _Project.Scripts._Infrastructure.AssetData;
using _Project.Scripts._Infrastructure.Factories.StateMachine;
using _Project.Scripts._Infrastructure.SceneLoader;
using _Project.Scripts._Infrastructure.Services.Logger;
using _Project.Scripts._Infrastructure.StateMachine.States;
using _Project.Scripts._Infrastructure.StaticData;
using _Project.Scripts._Infrastructure.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._Infrastructure.Scopes
{
  public class BootstrapScope : LifetimeScope
  {
    [SerializeField] private UIRootView _uiRootView;
   
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
      builder.RegisterComponentInNewPrefab(_uiRootView, Lifetime.Singleton);

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