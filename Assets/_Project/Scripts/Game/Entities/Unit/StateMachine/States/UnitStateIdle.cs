using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine.States
{
    public sealed class UnitStateIdle : UnitState, IState
    {
        public UnitStateIdle(IStateMachine stateMachine, UnitComponent unit) : base(stateMachine, unit)
        {
            
        }
        
        public void Enter()
        {
            Unit.Animator.OnRun.Execute(0f);
        }

        public void Tick() { }
        public void Exit() { }
    }
}