using _Project.Scripts.Game.Entities.Enemy.Components;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Entities.Enemy.StateMachine.States
{
    public sealed class EnemyStateFight : UnitState, IUnitState
    {
        private LevelModel _levelModel;

        private readonly float _aimingSpeed;
        private readonly float _attackInterval;

        public EnemyStateFight(IUnitStateMachine unitStateMachine, EnemyComponent enemy) : base(unitStateMachine, enemy)
        {
            _aimingSpeed = Enemy.WeaponMediator.CurrentWeapon.Weapon.AimingSpeed();
            _attackInterval = Enemy.WeaponMediator.CurrentWeapon.Weapon.AttackInterval();
        }

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
            Enemy.Agent.Agent.ResetPath();
            Enemy.Animator.OnRun.Execute(0f);
        }

        public void Exit() { }

        public void Tick()
        {
            if (IsDeath())
            {
                EnterState<EnemyStateDeath>();
                
                return;
            }
            
            LookAt();
            
            if (CanAttack())
            {
                Enemy.WeaponMediator.CurrentWeapon.Weapon.Attack(Enemy.Target);
                Enemy.Animator.OnAttack.Execute(_attackInterval);
            }
        }
        
        private void LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(_levelModel.Character.Position - Enemy.Position);

            Enemy.transform.rotation = Quaternion.Slerp(Enemy.transform.rotation, lookRotation, _aimingSpeed);
        }
        
        private bool CanAttack()
        {
            return Enemy.WeaponMediator.CurrentWeapon.Weapon.CanAttack() && 
                   HasFacingTarget();
        }
        
        private bool HasFacingTarget()
        {
            float angle = Vector3.Angle(Enemy.Forward.normalized, (_levelModel.Character.Position - Enemy.Position).normalized);

            return angle < 5f;
        }
        
        private bool IsDeath() => Enemy.Health.IsAlive == false;
    }
}