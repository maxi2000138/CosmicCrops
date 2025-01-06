using _Project.Scripts._Infrastructure.StateMachine.Machine;
using _Project.Scripts._Infrastructure.StateMachine.States;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._Infrastructure.Factories.StateMachine
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
                builder.Register<IState, StateGameplayLoop>(Lifetime.Scoped);
            });
                
            IGameStateMachine gameStateMachine;
            using (statesScope)
            {
                gameStateMachine = statesScope.Container.Resolve<IGameStateMachine>();
            }

            return gameStateMachine;
        }
    }
}