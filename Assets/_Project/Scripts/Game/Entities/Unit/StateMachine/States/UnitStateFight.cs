using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine.States
{
    public sealed class UnitStateFight : UnitState, IState
    {
        private const float AimingSpeed = 0.1f;

        private LevelModel _levelModel;

        public UnitStateFight(IStateMachine stateMachine, UnitComponent unit) : base(stateMachine, unit)
        {
            
        }

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
            Unit.Agent.Agent.ResetPath();
            Unit.UnitAnimator.OnRun.Execute(0f);
        }

        public void Exit() { }

        public void Tick()
        {
            LookAt();
        }
        
        private void LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(_levelModel.Character.Position - Unit.Position);

            Unit.transform.rotation = Quaternion.Slerp(Unit.transform.rotation, lookRotation, AimingSpeed);
        }
    }
}