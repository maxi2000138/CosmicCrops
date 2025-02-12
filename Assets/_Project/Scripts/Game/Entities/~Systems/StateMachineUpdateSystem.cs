using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;

namespace _Project.Scripts.Game.Entities._Systems
{
  public class StateMachineUpdateSystem : SystemComponent<StateMachineComponent>
  {
    protected override void OnUpdate()
    {
      base.OnUpdate();
            
      Components.Foreach(UpdateStateMachine);
    }

    private void UpdateStateMachine(StateMachineComponent stateMachine) => stateMachine.Execute();

  }
}