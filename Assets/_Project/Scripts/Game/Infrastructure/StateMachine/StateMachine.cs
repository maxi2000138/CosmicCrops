using System;
using System.Collections.Generic;
using _Project.Scripts.Game.Entities.Unit.StateMachine;
using _Project.Scripts.Infrastructure.Logger;

namespace _Project.Scripts.Game.Infrastructure.StateMachine
{
    public abstract class StateMachine : IStateMachine
    {
        private IState _activeState;

        public IReadOnlyDictionary<Type, IState> States { get; protected set; }
        
        void IStateMachine.Enter<T>()
        {
            T state = ChangeState<T>();

            state.Enter();
            
            if(this is UnitStateMachine)
                DebugLogger.Log("Enter Unit State: " + state.GetType().Name, LogsType.Character);
        }

        void IStateMachine.Tick() => _activeState?.Tick();

        private T ChangeState<T>() where T : class, IState
        {
            _activeState?.Exit();

            T state = GetState<T>();

            _activeState = state;
            
            return state;
        }

        private T GetState<T>() where T : class, IState => States[typeof(T)] as T;
    }
}