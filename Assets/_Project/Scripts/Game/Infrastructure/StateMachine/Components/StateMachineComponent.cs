using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Game.Infrastructure.StateMachine.Components
{
  public class StateMachineComponent : MonoComponent<StateMachineComponent>
  {
    public IStateMachine StateMachine { get; private set; }

    private bool _isExecute;
        
    public void CreateStateMachine(IStateMachine stateMachine)
    {
      StateMachine = stateMachine;
            
      _isExecute = true;
    }

    public void Execute()
    {
      if (_isExecute == false) return;

      StateMachine.Tick();
    }
  }
}