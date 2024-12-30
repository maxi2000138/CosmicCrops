using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.StateMachine.Machine
{
  public interface IEnterLoadState<in TLoad> : IExitState
  {
    UniTask Enter(TLoad load);
  }
}