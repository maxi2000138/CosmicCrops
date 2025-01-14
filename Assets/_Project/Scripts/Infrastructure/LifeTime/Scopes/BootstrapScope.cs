﻿using _Project.Scripts.Game.Level.Model;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Factories.StateMachine;
using _Project.Scripts.Infrastructure.Factories.Systems;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.Logger;
using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.StateMachine.States;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.UI.GUIService;
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
      
      builder.Register<DebugLogger>(Lifetime.Singleton);
      builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
      builder.Register<IAssetService, AssetService>(Lifetime.Singleton);
      builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
      builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Singleton);
      builder.Register<ISystemFactory, SystemFactory>(Lifetime.Singleton);
      builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
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