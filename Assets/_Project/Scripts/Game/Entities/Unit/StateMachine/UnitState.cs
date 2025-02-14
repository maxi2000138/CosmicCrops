using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine
{
  public class UnitState
  {
    private readonly IStateMachine _stateMachine;
        
    protected UnitComponent Unit { get; }

    protected UnitState(IStateMachine stateMachine, UnitComponent unit)
    {
      _stateMachine = stateMachine;
      Unit = unit;
    }

    protected void EnterState<T>() where T : UnitState, IState => _stateMachine.Enter<T>();

  }
}