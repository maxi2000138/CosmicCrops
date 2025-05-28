using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Utils.Extensions;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine.States
{
    public sealed class EnemyStateNone : UnitState, IUnitState
    {
        public EnemyStateNone(IUnitStateMachine unitStateMachine, EnemyComponent enemy) : base(unitStateMachine, enemy)
        {
        }

        public void Enter()
        {
            Enemy.Agent.Agent.ResetPath();
            Enemy.Animator.OnRun.Execute(0f);
            Enemy.CleanSubscribe();
        }

        public void Exit() { }
        public void Tick() { }
    }
}