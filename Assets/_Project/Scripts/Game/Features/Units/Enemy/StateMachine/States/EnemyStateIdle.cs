using _Project.Scripts.Game.Features.Units.Enemy.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Time;
using VContainer;

namespace _Project.Scripts.Game.Features.Units.Enemy.StateMachine.States
{
    public sealed class EnemyStateIdle : UnitState, IUnitState
    {
        private ITimeService _time;

        private float _delay;

        public EnemyStateIdle(IUnitStateMachine unitStateMachine, EnemyComponent enemy) : base(unitStateMachine, enemy)
        {
            
        }
        
        [Inject]
        private void Construct(ITimeService time)
        {
            _time = time;
        }
        
        public void Enter()
        {
            _delay = Enemy.Stats.StayDelay;

            Enemy.Animator.OnRun.Execute(0f);
        }

        public void Tick()
        {
            if (IsDeath())
            {
                EnterState<EnemyStateDeath>();
                
                return;
            }
            
            if (_delay > 0f)
            {
                _delay -= _time.DeltaTime;
            }
            else
            {
                EnterState<EnemyStatePatrol>();
            }
        }
        
        public void Exit() { }
        
        private bool IsDeath() => Enemy.Health.IsAlive == false;

    }
}