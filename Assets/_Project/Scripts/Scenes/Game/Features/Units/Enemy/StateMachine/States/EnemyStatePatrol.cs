using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Units.Enemy.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Utils.Extensions;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace _Project.Scripts.Game.Features.Units.Enemy.StateMachine.States
{
    public sealed class EnemyStatePatrol : UnitState, IUnitState
    {
        private LevelModel _levelModel;

        private readonly Vector3 _patrolPosition;
        private float _patrolRadius;
        private float _aggroRadius;
        

        public EnemyStatePatrol(IUnitStateMachine unitStateMachine, EnemyComponent enemy) : base(unitStateMachine, enemy)
        {
            _patrolPosition = enemy.Position;
        }
        
        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
            _patrolRadius = Enemy.Stats.PatrolRadius;
            _aggroRadius = Mathf.Pow(Enemy.WeaponMediator.CurrentWeapon.Weapon.DetectionDistance(), 2);
            
            Enemy.Agent.Agent.speed = Enemy.Stats.WalkSpeed;
            Enemy.Animator.OnRun.Execute(1f);

            Enemy.Agent.Agent.SetDestination(GeneratePointOnNavmesh());
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
            
            if (Enemy.Agent.Agent.hasPath)
            {
                return;
            }
        
            EnterState<EnemyStateIdle>();
        }

        private Vector3 GeneratePointOnNavmesh()
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 center = _patrolPosition + Mathematics.GenerateRandomPoint(_patrolRadius);
            
                if (NavMesh.SamplePosition(center, out NavMeshHit hit, 1f, 1))
                {
                    return hit.position;
                }
            }
            
            return Vector3.zero;
        }
        
        private float DistanceToTarget() => (_levelModel.Character.Position - Enemy.Position).sqrMagnitude;
        private bool CanPursuit() => DistanceToTarget() < _aggroRadius;
        private bool IsDeath() => Enemy.Health.IsAlive == false;
    }

}