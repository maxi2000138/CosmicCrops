using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.StateMachine.States
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