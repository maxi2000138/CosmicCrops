using _Project.Scripts._Infrastructure.StateMachine.Machine;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace _Project.Scripts._Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateLoadProgress : IEnterState
  {
    
    UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      gameStateMachine.Enter<StateLoadGameScene>();

      return UniTask.CompletedTask;
    }
  }
}