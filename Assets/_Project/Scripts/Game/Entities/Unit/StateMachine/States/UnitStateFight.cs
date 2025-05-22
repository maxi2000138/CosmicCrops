using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Utils;
using _Project.Scripts.Utils.Constants;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine.States
{
    public sealed class UnitStateFight : UnitState, IUnitState
    {
        private LevelModel _levelModel;

        private readonly float _attackDistance;
        private readonly float _aimingSpeed;
        private readonly float _attackInterval;

        public UnitStateFight(IUnitStateMachine unitStateMachine, UnitComponent unit) : base(unitStateMachine, unit)
        {
            _attackDistance = Unit.WeaponMediator.CurrentWeapon.Weapon.AttackDistance();
            _aimingSpeed = Unit.WeaponMediator.CurrentWeapon.Weapon.AimingSpeed();
            _attackInterval = Unit.WeaponMediator.CurrentWeapon.Weapon.AttackInterval();
        }

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
            Unit.Agent.Agent.ResetPath();
            Unit.Animator.OnRun.Execute(0f);
        }

        public void Exit() { }

        public void Tick()
        {
            if (IsDeath())
            {
                EnterState<UnitStateDeath>();
                
                return;
            }
            
            // if (CannotAttack())
            // {
            //     EnterState<UnitStatePursuit>();
            //
            //     return;
            // }
            //
            // LookAt();
            
            if (CanAttack())
            {
                Unit.WeaponMediator.CurrentWeapon.Weapon.Attack(_levelModel.Character);
                Unit.Animator.OnAttack.Execute(_attackInterval);
            }
        }
        
        private void LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(_levelModel.Character.Position - Unit.Position);

            Unit.transform.rotation = Quaternion.Slerp(Unit.transform.rotation, lookRotation, _aimingSpeed);
        }
        
        private bool CanAttack()
        {
            return Unit.WeaponMediator.CurrentWeapon.Weapon.CanAttack() && 
                   HasFacingTarget();
        }

        private bool CannotAttack()
        {
            return DistanceToTarget() > _attackDistance && _levelModel.Character.Health.IsAlive ||
                   HasObstacleOnAttackPath();
        }
        
        private float DistanceToTarget() => (_levelModel.Character.Position - Unit.Position).sqrMagnitude;
        
        private bool HasObstacleOnAttackPath()
        {
            return Physics.Linecast(Unit.Position, _levelModel.Character.Position, Layers.Wall);
        }

        private bool HasFacingTarget()
        {
            float angle = Vector3.Angle(Unit.Forward.normalized, (_levelModel.Character.Position - Unit.Position).normalized);

            return angle < 5f;
        }
        
        private bool IsDeath() => Unit.Health.IsAlive == false;
    }
}