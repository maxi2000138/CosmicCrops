using _Project.Scripts.Game.Infrastructure.StateMachine.Components;

namespace _Project.Scripts.Game.Units._Interfaces
{
  public interface IStateMachineComponent
  {
    StateMachineComponent StateMachine { get; }
  }
}