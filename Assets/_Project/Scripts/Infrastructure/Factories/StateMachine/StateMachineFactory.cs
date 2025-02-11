using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Game.Level.Components;
using _Project.Scripts.Game.Units.Character.Components;
using _Project.Scripts.Game.Units.Character.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine.States;
using _Project.Scripts.Utils.Extensions;
using VContainer;
using VContainer.Unity;
using IState = _Project.Scripts.Infrastructure.StateMachine.States.Interfaces.IState;

namespace _Project.Scripts.Infrastructure.Factories.StateMachine
{
    public sealed class StateMachineFactory : IStateMachineFactory
    {
        private readonly LifetimeScope _lifetimeScope;

        public StateMachineFactory(LifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        IGameStateMachine IStateMachineFactory.CreateGameStateMachine()
        {
            LifetimeScope statesScope = _lifetimeScope.CreateChild(builder => {
                builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Scoped);
                
                builder.Register<IState, StateBootstrap>(Lifetime.Scoped);
                builder.Register<IState, StateLoadProgress>(Lifetime.Scoped);
                
                builder.Register<IState, StateLoadGameScene>(Lifetime.Scoped);
                builder.Register<IState, StateLobby>(Lifetime.Scoped);
                builder.Register<IState, StateGameplay>(Lifetime.Scoped);
            });
                
            IGameStateMachine gameStateMachine;
            using (statesScope)
            {
                gameStateMachine = statesScope.Container.Resolve<IGameStateMachine>();
            }

            return gameStateMachine;
        }
        
        public IStateMachine CreateCharacterStateMachine(CharacterComponent character)
        {
            CharacterStateMachine characterStateMachine = new CharacterStateMachine(character);
            characterStateMachine.States.Values.Foreach(_lifetimeScope.Container.Inject);
            return characterStateMachine;
        }
    }
}