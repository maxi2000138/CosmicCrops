using _Project.Scripts._Infrastructure.StateMachine.Machine;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts._Infrastructure.StateMachine.States
{
  public class StateGameplayLoop : IEnterState
  {

    public UniTask Enter(IGameStateMachine gameStateMachine)
    {
      return UniTask.CompletedTask;
    }
  }
}