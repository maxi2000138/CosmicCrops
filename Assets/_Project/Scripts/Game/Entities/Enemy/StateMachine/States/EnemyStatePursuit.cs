using _Project.Scripts.Game.Entities.Enemy.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Enemy.StateMachine.States
{
    public sealed class EnemyStatePursuit : UnitState, IUnitState
    {
        public EnemyStatePursuit(IUnitStateMachine unitStateMachine, EnemyComponent enemy) : base(unitStateMachine, enemy)
        {
            
        }
        
        public void Enter()
        {
            Enemy.Agent.Agent.speed = Enemy.Stats.PursuitSpeed;
            Enemy.Animator.OnRun.Execute(1f);
        }

        public void Exit()
        {
            Enemy.Agent.Agent.ResetPath();
        }

        public void Tick()
        {
            if (IsDeath())
            {
                EnterState<EnemyStateDeath>();
                
                return;
            }

            if (Enemy.Target == null)
            {
                EnterState<EnemyStateIdle>();
            }
            else
            {
                Enemy.Agent.Agent.SetDestination(Enemy.Target.Position);
            }
        }

        private bool IsDeath() => Enemy.Health.IsAlive == false;
    }
}