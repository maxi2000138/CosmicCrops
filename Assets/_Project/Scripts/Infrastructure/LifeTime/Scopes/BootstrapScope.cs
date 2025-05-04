using System;
using _Project.Scripts.Game.Entities.Character._Configs;
using _Project.Scripts.Game.Entities.Unit._Configs;
using _Project.Scripts.Game.Features.Abilities._Configs;
using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Collector._Configs;
using _Project.Scripts.Game.Features.Collector.Factory;
using _Project.Scripts.Game.Features.Inventory;
using _Project.Scripts.Game.Features.Level._Configs;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Loot._Configs;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon.Factories;
using _Project.Scripts.Game.UI._Configs;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Factories.StateMachine;
using _Project.Scripts.Infrastructure.Factories.Systems;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.GUI;
using _Project.Scripts.Infrastructure.Haptic;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.Logger;
using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.StateMachine.States;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Infrastructure.Time;
using _Project.Scripts.Utils.Extensions;
using _Project.Scripts.Utils.PartLinears;
using CodeBase.Infrastructure.Pool;
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

      builder.RegisterComponentInNewPrefab(_loadingCurtain, Lifetime.Singleton).UnderTransform(transform).As<ILoadingCurtainService>();
      builder.RegisterComponentInNewPrefab(_cameraService, Lifetime.Singleton).UnderTransform(transform).As<ICameraService>();
      builder.RegisterComponentInNewPrefab(_guiService, Lifetime.Singleton).UnderTransform(transform).As<IGuiService>();
      builder.RegisterComponentInNewPrefab(_joystickService, Lifetime.Singleton).UnderTransform(transform).As<IJoystickService>();
      
      builder.Register<LevelModel>(Lifetime.Singleton);
      builder.Register<InventoryModel>(Lifetime.Singleton);
      
      builder.Register<DebugLogger>(Lifetime.Singleton);
      builder.Register<ITimeService, TimeService>(Lifetime.Singleton);
      builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
      builder.Register<IWeaponFactory, WeaponFactory>(Lifetime.Singleton);
      builder.Register<IAssetService, AssetService>(Lifetime.Singleton);
      builder.Register<IConfigsLoader, ConfigsLoader>(Lifetime.Singleton);
      builder.Register<IHapticService, HapticService>(Lifetime.Singleton);
      builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
      builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Singleton);
      builder.Register<ISystemFactory, SystemFactory>(Lifetime.Singleton);
      builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
      builder.Register<ICollectorFactory, CollectorFactory>(Lifetime.Singleton);
      builder.Register<IObjectPoolService, ObjectPoolService>(Lifetime.Singleton).WithParameter(transform).As<IDisposable>();
      builder.Register<IStateMachineFactory, StateMachineFactory>(Lifetime.Singleton);
      builder.Register<IAbilityApplier, AbilityApplier>(Lifetime.Singleton);
      builder.Register<IAbilityStatsProvider, AbilityStatsProvider>(Lifetime.Singleton);
      
      builder.RegisterConfig<PartLinearsConfig>(Lifetime.Singleton);
      builder.RegisterConfig<ProjectilesConfig>(Lifetime.Singleton);
      builder.RegisterConfig<CharacterConfig>(Lifetime.Singleton);
      builder.RegisterConfig<UIPrefabsConfig>(Lifetime.Singleton);
      builder.RegisterConfig<ScreensConfig>(Lifetime.Singleton);
      builder.RegisterConfig<WeaponsConfig>(Lifetime.Singleton);
      builder.RegisterConfig<UnitsConfig>(Lifetime.Singleton);
      builder.RegisterConfig<LevelConfig>(Lifetime.Singleton);
      builder.RegisterConfig<LootConfig>(Lifetime.Singleton);
      builder.RegisterConfig<AbilitiesConfig>(Lifetime.Singleton);
      builder.RegisterConfig<EffectsConfig>(Lifetime.Singleton);
      builder.RegisterConfig<CollectorsConfig>(Lifetime.Singleton);

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