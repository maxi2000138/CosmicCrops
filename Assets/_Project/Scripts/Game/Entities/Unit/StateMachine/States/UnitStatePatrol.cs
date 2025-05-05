using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine.States
{
    public sealed class UnitStatePatrol : UnitState, IUnitState
    {
        private LevelModel _levelModel;

        private readonly Vector3 _patrolPosition;
        private float _patrolRadius;
        private float _aggroRadius;
        

        public UnitStatePatrol(IUnitStateMachine unitStateMachine, UnitComponent unit) : base(unitStateMachine, unit)
        {
            _patrolPosition = unit.Position;
        }
        
        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
            _patrolRadius = Unit.Stats.PatrolRadius;
            _aggroRadius = Mathf.Pow(Unit.WeaponMediator.Weapon.Weapon.DetectionDistance(), 2);
            
            Unit.Agent.Agent.speed = Unit.Stats.WalkSpeed;
            Unit.Animator.OnRun.Execute(1f);

            Unit.Agent.Agent.SetDestination(GeneratePointOnNavmesh());
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
            
            if (CanPursuit())
            {
                EnterState<UnitStatePursuit>();
            }
            else
            {
                if (Unit.Agent.Agent.hasPath)
                {
                    return;
                }

                EnterState<UnitStateIdle>();
            }
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
        
        private float DistanceToTarget() => (_levelModel.Character.Position - Unit.Position).sqrMagnitude;
        private bool CanPursuit() => DistanceToTarget() < _aggroRadius;
        private bool IsDeath() => Unit.Health.IsAlive == false;
    }

}