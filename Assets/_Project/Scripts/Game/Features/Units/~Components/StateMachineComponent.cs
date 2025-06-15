using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Game.Features.Units._Components
{
  public class StateMachineComponent : MonoComponent<StateMachineComponent>
  {
    public IUnitStateMachine UnitStateMachine { get; private set; }

    private bool _isExecute;
        
    public void CreateStateMachine(IUnitStateMachine unitStateMachine)
    {
      UnitStateMachine = unitStateMachine;
            
      _isExecute = true;
    }

    public void Execute()
    {
      if (_isExecute == false) return;

      UnitStateMachine.Tick();
    }
  }
}