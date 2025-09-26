using _Project.Scripts.Game.Features.Units._Components;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;

namespace _Project.Scripts.Game.Features.Units._Systems
{
  public class UnitStateMachineUpdateSystem : SystemComponent<StateMachineComponent>
  {
    protected override void OnUpdate()
    {
      base.OnUpdate();
            
      Components.Foreach(UpdateStateMachine);
    }

    private void UpdateStateMachine(StateMachineComponent stateMachine) => stateMachine.Execute();

  }
}