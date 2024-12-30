using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.StateMachine.Machine
{
  public interface IExitState
  {
    UniTask Exit();
  }
}