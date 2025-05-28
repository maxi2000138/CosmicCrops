using _Project.Scripts.Game._Editor;
using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.AI.Services;
using _Project.Scripts.Game.Features.AI.Services.AIReporter;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI.Calculations;
using _Project.Scripts.Game.Features.Collector.Factory;
using _Project.Scripts.Game.Features.Inventory;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Weapon.Services.Factories;
using _Project.Scripts.Game.Infrastructure.Systems;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Factories.StateMachine;
using _Project.Scripts.Infrastructure.Factories.Systems;
using _Project.Scripts.Infrastructure.LifeTime.EntryPoints;
using _Project.Scripts.Infrastructure.StateMachine.States;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.LifeTime.Scopes
{
  public class GameScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      base.Configure(builder);

      builder.RegisterEntryPoint<SystemsContainer>();
      builder.RegisterEntryPoint<GameEntryPoint>();
      
      builder.Register<LevelModel>(Lifetime.Singleton);
      builder.Register<InventoryModel>(Lifetime.Singleton);

      builder.Register<UtilityAICalculations>(Lifetime.Singleton);
      builder.Register<ITargetPicker, TargetPicker>(Lifetime.Singleton);
      builder.Register<IAIReporter, AIReporter>(Lifetime.Singleton);
      builder.Register<IArtificialIntelligence, UtilityAI>(Lifetime.Singleton);
      builder.Register<IAbilityStatsProvider, AbilityStatsProvider>(Lifetime.Singleton);
      builder.Register<ICollectorFactory, CollectorFactory>(Lifetime.Singleton);
      builder.Register<IAbilityApplier, AbilityApplier>(Lifetime.Singleton);
      builder.Register<ISystemFactory, SystemFactory>(Lifetime.Singleton);

      builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
      builder.Register<IWeaponFactory, WeaponFactory>(Lifetime.Singleton);
      builder.Register<IStateMachineFactory, StateMachineFactory>(Lifetime.Singleton);

      
      builder.Register<IState, StateGameBootstrap>(Lifetime.Singleton);
      builder.Register<IState, StateLobby>(Lifetime.Singleton);
      builder.Register<IState, StateGame>(Lifetime.Singleton);
      builder.Register<IState, StateGameResult>(Lifetime.Singleton);
    }
  }
}