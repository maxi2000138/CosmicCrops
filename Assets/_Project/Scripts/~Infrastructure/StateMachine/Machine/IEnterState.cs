using Cysharp.Threading.Tasks;
namespace _Project.Scripts._Infrastructure.StateMachine.Machine
{
  public interface IEnterState : IState
  {
    public UniTask Enter(IGameStateMachine gameStateMachine);
  }
}