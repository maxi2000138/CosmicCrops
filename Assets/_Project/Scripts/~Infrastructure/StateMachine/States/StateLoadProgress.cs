using _Project.Scripts._Infrastructure.StateMachine.Machine;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts._Infrastructure.StateMachine.States
{
  public sealed class StateLoadProgress : IEnterState
  {
    
    UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      gameStateMachine.Enter<StateLoadGameScene>();

      return UniTask.CompletedTask;
    }
  }
}