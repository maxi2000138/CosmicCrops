using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.StateMachine.Machine
{
  public interface IEnterState : IExitState
  {
    UniTask Enter();
  }
}