using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Character.StateMachine;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Entities.Unit.StateMachine;
using _Project.Scripts.Game.Infrastructure.StateMachine;
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
                builder.Register<IState, StateLoadConfigs>(Lifetime.Scoped);
                
                builder.Register<IState, StateMenu>(Lifetime.Scoped);
                builder.Register<IState, StateLoadGameScene>(Lifetime.Scoped);
                builder.Register<IState, StateLobby>(Lifetime.Scoped);
                builder.Register<IState, StateGame>(Lifetime.Scoped);
                builder.Register<IState, StateGameResult>(Lifetime.Scoped);
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
        
        public IStateMachine CreateUnitStateMachine(UnitComponent unit)
        {
            UnitStateMachine unitStateMachine = new UnitStateMachine(unit);
            unitStateMachine.States.Values.Foreach(_lifetimeScope.Container.Inject);
            return unitStateMachine;
        }
    }
}