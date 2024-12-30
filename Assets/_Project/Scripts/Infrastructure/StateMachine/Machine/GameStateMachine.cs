using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure.Services.Logger;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.StateMachine.Machine
{
  public sealed class GameStateMachine : IGameStateMachine
  {
    public IReadOnlyDictionary<Type, IExitState> States { get; private set; }
    
    private IExitState _activeState;

    public void Construct(Dictionary<Type, IExitState> states)
    {
      States = states;
    }

    async UniTask IGameStateMachine.Enter<TState>()
    {
      TState state = ChangeState<TState>();
      DebugLogger.Log("Enter state " + state.GetType().Name, LogsType.Infrastructure);
      await state.Enter();
    }

    async UniTask IGameStateMachine.Enter<TState, TLoad>(TLoad load)
    {
      TState state = ChangeState<TState>();
      DebugLogger.Log("Enter state " + state.GetType().Name, LogsType.Infrastructure);
      await state.Enter(load);
    }

    private TState ChangeState<TState>() where TState : class, IExitState
    {
      _activeState?.Exit();
      TState state = GetState<TState>();
      _activeState = state;
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitState => States[typeof(TState)] as TState;
  }
}