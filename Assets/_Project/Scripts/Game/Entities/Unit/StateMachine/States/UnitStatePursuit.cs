using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine.States
{
    public sealed class UnitStatePursuit : UnitState, IState
    {
        
        public UnitStatePursuit(IStateMachine stateMachine, UnitComponent unit) : base(stateMachine, unit)
        {
        }
        
        public void Enter()
        {
            Unit.UnitAnimator.OnRun.Execute(1f);
        }

        public void Exit()
        {
            Unit.Agent.Agent.ResetPath();
        }

        public void Tick() { }
    }
}