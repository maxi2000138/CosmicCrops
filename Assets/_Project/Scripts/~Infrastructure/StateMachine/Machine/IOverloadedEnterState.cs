using Cysharp.Threading.Tasks;
namespace _Project.Scripts._Infrastructure.StateMachine.Machine
{
  public interface IOverloadedEnterState<in T> : IState
  {
    public UniTask Enter(IGameStateMachine gameStateMachine, T overload);
  }
}
