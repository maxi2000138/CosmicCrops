using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine
{
  public class UnitState
  {
    private readonly IUnitStateMachine _unitStateMachine;
        
    protected UnitComponent Unit { get; }

    protected UnitState(IUnitStateMachine unitStateMachine, UnitComponent unit)
    {
      _unitStateMachine = unitStateMachine;
      Unit = unit;
    }

    protected void EnterState<T>() where T : UnitState, IUnitState => _unitStateMachine.Enter<T>();

  }
}