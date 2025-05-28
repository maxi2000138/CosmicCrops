using _Project.Scripts.Game.Entities.Enemy.Components;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Utils.Extensions;
using DG.Tweening;
using VContainer;

namespace _Project.Scripts.Game.Entities.Enemy.StateMachine.States
{
    public sealed class EnemyStateDeath : UnitState, IUnitState
    {
        private LevelModel _levelModel;

        public EnemyStateDeath(IUnitStateMachine unitStateMachine, EnemyComponent enemy) : base(unitStateMachine, enemy)
        {
        }

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }
        
        public void Enter()
        {
            Enemy.Agent.Agent.ResetPath();
            Enemy.Animator.OnDeath.Execute(R3.Unit.Default);
            Enemy.CleanSubscribe();

            DeactivateUnit();

            _levelModel.RemoveEnemy(Enemy);
        }

        public void Tick() { }
        public void Exit() { }


        private void DeactivateUnit()
        {
            DOVirtual.DelayedCall(1.5f, () => Enemy.Remove()).SetLink(Enemy.gameObject);
        }
    }
}