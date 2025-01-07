using System;
using System.Collections.Generic;
using _Project.Scripts._Infrastructure.Logger;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer.Unity;

namespace _Project.Scripts._Infrastructure.StateMachine.Machine
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IState> _states = new();
        private IState _currentState;

        public GameStateMachine(IEnumerable<IState> states)
        {
            foreach (IState state in states) 
                AddState(state);
        }

        public void Tick()
        {
            if(_currentState is ITickable tickableState)
                tickableState.Tick();
        }
        
        public void FixedTick()
        {
            if(_currentState is IFixedTickable fixedTickableState)
                fixedTickableState.FixedTick();
        }
        
        public async UniTask Enter<TState>() where TState : IState
        {
            DebugLogger.Log($"Enter {typeof(TState).Name}", LogsType.Infrastructure);

            await TryExitStateAndSetCurrent(GetState<TState>());

            if (_currentState is IEnterState enterState)
                await enterState.Enter(this);
        }

        public async UniTask Enter<TState, TOverload>(TOverload overload) where TState : IOverloadedEnterState<TOverload>
        {
            DebugLogger.Log($"Enter {typeof(TState).Name} with overload {overload}", LogsType.Infrastructure);

            await TryExitStateAndSetCurrent(GetState<TState>());

            if (_currentState is IOverloadedEnterState<TOverload> enterState)
                await enterState.Enter(this, overload);
        }
        
        private async UniTask TryExitStateAndSetCurrent(IState state)
        {
            if (_currentState is IExitState exitState)
                await exitState.Exit(this);

            _currentState = state;
        }

        private IState GetState<T>()
        {
            if (!_states.TryGetValue(typeof(T), out IState state))
                throw new ArgumentOutOfRangeException(typeof(T).Name, $"State '{typeof(T).Name}' not found in states.");

            return state;
        }
        
        private void AddState(IState state)
        {
            _states.Add(state.GetType(), state);
        }

        private void RemoveState(Type stateType)
        {
            if (!_states.Remove(stateType))
                throw new ArgumentException($"State '{stateType}' not found.");
        }
    }
}