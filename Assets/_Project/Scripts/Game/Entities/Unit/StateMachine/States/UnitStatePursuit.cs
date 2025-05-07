using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Utils;
using _Project.Scripts.Utils.Constants;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine.States
{
    public sealed class UnitStatePursuit : UnitState, IUnitState
    {
        private LevelModel _levelModel;

        private float _pursuitRadius;
        private float _attackDistance;
        
        public UnitStatePursuit(IUnitStateMachine unitStateMachine, UnitComponent unit) : base(unitStateMachine, unit)
        {
        }
        
        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
            _pursuitRadius = Mathf.Pow(Unit.Stats.PursuitRadius, 2);
            _attackDistance = Unit.WeaponMediator.CurrentWeapon.Weapon.AttackDistance();
            Unit.Agent.Agent.speed = Unit.Stats.PursuitSpeed;
            Unit.Animator.OnRun.Execute(1f);
        }

        public void Exit()
        {
            Unit.Agent.Agent.ResetPath();
        }

        public void Tick()
        {
            if (IsDeath())
            {
                EnterState<UnitStateDeath>();
                
                return;
            }

            if (CanIdle())
            {
                EnterState<UnitStateIdle>();
            }
            else
            {
                if (CanFight())
                {
                    EnterState<UnitStateFight>();
                }
                else
                {
                    Unit.Agent.Agent.SetDestination(_levelModel.Character.Position);
                }
            }
        }

        private bool HasObstacleOnAttackPath() => Physics.Linecast(Unit.Position, _levelModel.Character.Position, Layers.Wall);

        private bool CanIdle() => DistanceToTarget() > _pursuitRadius;
        private bool CanFight() => DistanceToTarget() < _attackDistance && HasObstacleOnAttackPath() == false;
        private float DistanceToTarget() => (_levelModel.Character.Position - Unit.Position).sqrMagnitude;
        private bool IsDeath() => Unit.Health.IsAlive == false;
    }
}