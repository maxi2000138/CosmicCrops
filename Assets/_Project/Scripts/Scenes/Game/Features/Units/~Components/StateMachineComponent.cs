using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Game.UI.Pause.Interface;
using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Game.Features.Units._Components
{
  public class StateMachineComponent : MonoComponent<StateMachineComponent>, IPause
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
    
    public void Pause(bool isPause)
    {
      _isExecute = !isPause;
    }
  }
}