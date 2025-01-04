using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _Project.Scripts._Infrastructure.StateMachine.Machine
{
  public interface IGameStateMachine : ITickable, IFixedTickable
  {
    UniTask Enter<TState>() where TState : IState;
    UniTask Enter<TState, TOverload>(TOverload overload) where TState : IOverloadedEnterState<TOverload>;
  }
}