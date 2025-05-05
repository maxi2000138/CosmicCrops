using _Project.Scripts.Infrastructure.Factories.Systems;
using _Project.Scripts.Infrastructure.LifeTime.EntryPoints.Core;
using _Project.Scripts.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine.States;
using _Project.Scripts.Utils.Extensions;
using JetBrains.Annotations;
using VContainer;

namespace _Project.Scripts.Infrastructure.LifeTime.EntryPoints
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class GameEntryPoint : EntryPointSystemBase
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ISystemFactory _systemFactory;
        private readonly IGameStateMachine _gameStateMachine;

        public GameEntryPoint(IObjectResolver objectResolver, ISystemFactory systemFactory, IGameStateMachine gameStateMachine)
        {
            _objectResolver = objectResolver;
            _systemFactory = systemFactory;
            _gameStateMachine = gameStateMachine;
        }
        
        public override void Initialize()
        {
            base.Initialize();
            
            Systems = _systemFactory.CreateGameSystems();
            Systems.Foreach(_objectResolver.Inject);
        }

        public override void Entry()
        {
            base.Entry();
            
            _gameStateMachine.Enter<StateGameBootstrap>();
        }
    }
}