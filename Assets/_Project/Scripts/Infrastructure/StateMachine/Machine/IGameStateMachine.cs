using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.StateMachine.Machine
{
  public interface IGameStateMachine
  {
    UniTask Enter<TState>() where TState : class, IEnterState;
    UniTask Enter<TState, TLoad>(TLoad load) where TState : class, IEnterLoadState<TLoad>;
  }


}