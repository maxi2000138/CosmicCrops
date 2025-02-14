using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine.States
{
    public sealed class UnitStatePatrol : UnitState, IState
    {
        public UnitStatePatrol(IStateMachine stateMachine, UnitComponent unit) : base(stateMachine, unit)
        {
            
        }

        public void Enter()
        {
            Unit.Animator.OnRun.Execute(1f);
        }

        public void Exit()
        {
            Unit.Agent.Agent.ResetPath();
        }

        public void Tick() { }
    }
}