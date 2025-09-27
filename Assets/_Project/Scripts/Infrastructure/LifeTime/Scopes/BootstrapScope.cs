using System;
using _Project.Scripts.Game.Features.Abilities._Configs;
using _Project.Scripts.Game.Features.Collector._Configs;
using _Project.Scripts.Game.Features.Inventory;
using _Project.Scripts.Game.Features.Level._Configs;
using _Project.Scripts.Game.Features.Loot._Configs;
using _Project.Scripts.Game.Features.Units.Enemy._Configs;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.UI._Configs;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.GUI;
using _Project.Scripts.Infrastructure.Haptic;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.LifeTime.EntryPoints;
using _Project.Scripts.Infrastructure.Logger;
using _Project.Scripts.Infrastructure.Pool;
using _Project.Scripts.Infrastructure.Progress;
using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine.States;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.StaticData.Configs.Loader;
using _Project.Scripts.Infrastructure.Time;
using _Project.Scripts.Scenes.Game.Common._Config;
using _Project.Scripts.Utils.Extensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace _Project.Scripts.Infrastructure.LifeTime.Scopes
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

      builder.RegisterEntryPoint<BootstrapEntryPoint>();
      builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Scoped);
      
      builder.RegisterComponentInNewPrefab(_loadingCurtain, Lifetime.Singleton).UnderTransform(transform).As<ILoadingCurtainService>();
      builder.RegisterComponentInNewPrefab(_cameraService, Lifetime.Singleton).UnderTransform(transform).As<ICameraService>();
      builder.RegisterComponentInNewPrefab(_guiService, Lifetime.Singleton).UnderTransform(transform).As<IGuiService>();
      builder.RegisterComponentInNewPrefab(_joystickService, Lifetime.Singleton).UnderTransform(transform).As<IJoystickService>();
      
      builder.Register<InventoryModel>(Lifetime.Singleton);
      
      builder.Register<DebugLogger>(Lifetime.Singleton);
      builder.Register<ITimeService, TimeService>(Lifetime.Singleton);
      builder.Register<IProgressService, ProgressService>(Lifetime.Singleton);
      builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
      builder.Register<IAssetDownloadReporter, AssetDownloadReporter>(Lifetime.Singleton);
      builder.Register<IAssetDownloadService, AssetDownloadService>(Lifetime.Singleton);
      builder.Register<IConfigsLoader, ConfigsLoader>(Lifetime.Singleton);
      builder.Register<IHapticService, HapticService>(Lifetime.Singleton);
      builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
      builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Singleton);
      builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
      builder.Register<IObjectPoolService, ObjectPoolService>(Lifetime.Singleton).WithParameter(transform).As<IDisposable>();
      
      builder.RegisterConfig<CommonConstantsConfig>(Lifetime.Singleton);
      builder.RegisterConfig<ProjectilesConfig>(Lifetime.Singleton);
      builder.RegisterConfig<UIPrefabsConfig>(Lifetime.Singleton);
      builder.RegisterConfig<ScreensConfig>(Lifetime.Singleton);
      builder.RegisterConfig<WeaponsConfig>(Lifetime.Singleton);
      builder.RegisterConfig<EnemiesConfig>(Lifetime.Singleton);
      builder.RegisterConfig<LevelConfig>(Lifetime.Singleton);
      builder.RegisterConfig<LootConfig>(Lifetime.Singleton);
      builder.RegisterConfig<AbilitiesConfig>(Lifetime.Singleton);
      builder.RegisterConfig<EffectsConfig>(Lifetime.Singleton);
      builder.RegisterConfig<CollectorsConfig>(Lifetime.Singleton);

      builder.Register<IState, StateProjectBootstrap>(Lifetime.Singleton);
      builder.Register<IState, StateLoadStaticData>(Lifetime.Singleton);
      builder.Register<IState, StateLoadProgress>(Lifetime.Singleton);
      builder.Register<IState, StateInitGameServices>(Lifetime.Singleton);
      builder.Register<IState, StateLoadMenu>(Lifetime.Singleton);
      builder.Register<IState, StateLoadGame>(Lifetime.Singleton);
      
      builder.RegisterBuildCallback(ResolveNonLaziesServices);
    }

    private void ResolveNonLaziesServices(IObjectResolver container)
    {
      container.Resolve<DebugLogger>();
    }
  }
}