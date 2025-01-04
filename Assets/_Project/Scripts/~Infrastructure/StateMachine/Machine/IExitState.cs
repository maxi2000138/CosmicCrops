using Cysharp.Threading.Tasks;
namespace _Project.Scripts._Infrastructure.StateMachine.Machine
{
  public interface IExitState : IState
  {
    public UniTask Exit(IGameStateMachine gameStateMachine);
  }
}