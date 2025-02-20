using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine.States
{
    public sealed class UnitStateNone : UnitState, IState
    {
        public UnitStateNone(IStateMachine stateMachine, UnitComponent unit) : base(stateMachine, unit)
        {
        }

        public void Enter()
        {
            Unit.Agent.Agent.ResetPath();
            Unit.Animator.OnRun.Execute(0f);
            Unit.CleanSubscribe();
        }

        public void Exit() { }
        public void Tick() { }
    }
}