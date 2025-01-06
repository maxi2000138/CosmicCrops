using _Project.Scripts._Infrastructure.AssetData;
using _Project.Scripts._Infrastructure.Camera;
using _Project.Scripts._Infrastructure.Factories.StateMachine;
using _Project.Scripts._Infrastructure.Factories.Systems;
using _Project.Scripts._Infrastructure.Input;
using _Project.Scripts._Infrastructure.SceneLoader;
using _Project.Scripts._Infrastructure.Services.Logger;
using _Project.Scripts._Infrastructure.StateMachine.States;
using _Project.Scripts._Infrastructure.StaticData;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.GUI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._Infrastructure.LifeTime.Scopes
{
  public class BootstrapScope : LifetimeScope
  {
    [SerializeField] private LoadingCurtain _loadingCurtain;
    [SerializeField] private CameraService _cameraService;
    [SerializeField] private GuiService _guiService;
    [SerializeField] private JoystickService _joystickService;
   
    protected override void Awake()
    {
      base.Awake();
      DontDestroyOnLoad(this);
    }

    protected override void Configure(IContainerBuilder builder)
    {
      base.Configure(builder);

      builder.RegisterComponentInNewPrefab(_loadingCurtain, Lifetime.Singleton).UnderTransform(transform).As<ILoadingCurtainService>();
      builder.RegisterComponentInNewPrefab(_cameraService, Lifetime.Singleton).UnderTransform(transform).As<ICameraService>();
      builder.RegisterComponentInNewPrefab(_guiService, Lifetime.Singleton).UnderTransform(transform).As<IGuiService>();
      builder.RegisterComponentInNewPrefab(_joystickService, Lifetime.Singleton).UnderTransform(transform).As<IJoystickService>();
      
      builder.Register<DebugLogger>(Lifetime.Singleton);
      builder.Register<IAssetService, AssetService>(Lifetime.Singleton);
      builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
      builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Singleton);
      builder.Register<ISystemFactory, SystemFactory>(Lifetime.Singleton);
      builder.Register<IStateMachineFactory, StateMachineFactory>(Lifetime.Singleton);
      
      builder.RegisterBuildCallback(ResolveNonLaziesServices);
      builder.RegisterBuildCallback(EnterGameStateMachine);
    }
    
    private void EnterGameStateMachine(IObjectResolver container)
    {
      container.Resolve<IStateMachineFactory>().CreateGameStateMachine().Enter<StateBootstrap>();
    }

    private void ResolveNonLaziesServices(IObjectResolver container)
    {
      container.Resolve<DebugLogger>();
    }
  }
}