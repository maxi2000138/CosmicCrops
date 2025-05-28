using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine
{
  public class UnitState
  {
    private readonly IUnitStateMachine _unitStateMachine;
        
    protected EnemyComponent Enemy { get; }

    protected UnitState(IUnitStateMachine unitStateMachine, EnemyComponent enemy)
    {
      _unitStateMachine = unitStateMachine;
      Enemy = enemy;
    }

    protected void EnterState<T>() where T : UnitState, IUnitState => _unitStateMachine.Enter<T>();

  }
}