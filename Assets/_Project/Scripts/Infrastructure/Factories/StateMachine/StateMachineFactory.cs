using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure.StateMachine.Machine;
using _Project.Scripts.Infrastructure.StateMachine.States;
using JetBrains.Annotations;
using Sirenix.Utilities;
using VContainer;

namespace _Project.Scripts.Infrastructure.Factories.StateMachine
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StateMachineFactory : IStateMachineFactory
    {
        private readonly IObjectResolver _objectResolver;

        public StateMachineFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        IGameStateMachine IStateMachineFactory.CreateGameStateMachine(string startScene)
        {
            GameStateMachine gameStateMachine = new GameStateMachine();
            
            var states = new Dictionary<Type, IExitState>()
            {
                {typeof(StateBootstrap), new StateBootstrap(gameStateMachine)},
                {typeof(StateLoadProgress), new StateLoadProgress(gameStateMachine)},
                
                {typeof(StateLoadTargetScene), new StateLoadTargetScene(gameStateMachine, startScene)},
                {typeof(StateLoadGameScene), new StateLoadGameScene(gameStateMachine)},
                {typeof(StateLoadMenuScene), new StateLoadMenuScene(gameStateMachine)},
            };
            
            gameStateMachine.Construct(InjectedStates(states));
            return gameStateMachine;
        }

        private Dictionary<Type,IExitState> InjectedStates(Dictionary<Type,IExitState> states)
        {
            states.Values.ForEach(_objectResolver.Inject);
            return states;
        }
    }
}