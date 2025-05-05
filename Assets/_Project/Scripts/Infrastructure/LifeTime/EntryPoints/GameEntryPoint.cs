using _Project.Scripts.Infrastructure.Factories.Systems;
using _Project.Scripts.Infrastructure.LifeTime.EntryPoints.Core;
using _Project.Scripts.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine.States;
using _Project.Scripts.Utils.Extensions;
using JetBrains.Annotations;
using VContainer;

namespace _Project.Scripts.Infrastructure.LifeTime.EntryPoints
{
    public sealed class GameEntryPoint : EntryPointBase
    {
        private readonly IGameStateMachine _gameStateMachine;

        public GameEntryPoint(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        protected override void Entry()
        {
            base.Entry();
            
            _gameStateMachine.Enter<StateGameBootstrap>();
        }
    }
}