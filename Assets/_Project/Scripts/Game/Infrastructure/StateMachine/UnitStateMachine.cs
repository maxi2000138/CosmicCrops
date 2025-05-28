using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure.Logger;

namespace _Project.Scripts.Game.Infrastructure.StateMachine
{
    public abstract class UnitStateMachine : IUnitStateMachine
    {
        private IUnitState _activeUnitState;

        public IReadOnlyDictionary<Type, IUnitState> States { get; protected set; }
        public IUnitState CurrentState => _activeUnitState;
        
        void IUnitStateMachine.Enter<T>()
        {
            T state = ChangeState<T>();

            state.Enter();
            
            DebugLogger.Log("Enter Unit State: " + state.GetType().Name, LogsType.Character);
        }

        void IUnitStateMachine.Tick() => _activeUnitState?.Tick();

        private T ChangeState<T>() where T : class, IUnitState
        {
            _activeUnitState?.Exit();

            T state = GetState<T>();

            _activeUnitState = state;
            
            return state;
        }

        private T GetState<T>() where T : class, IUnitState => States[typeof(T)] as T;
    }
}