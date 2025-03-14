﻿using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine.States
{
    public sealed class UnitStateIdle : UnitState, IState
    {
        private LevelModel _levelModel;

        private float _detectionDistance;
        private float _delay;

        public UnitStateIdle(IStateMachine stateMachine, UnitComponent unit) : base(stateMachine, unit)
        {
            
        }
        
        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }
        
        public void Enter()
        {
            _detectionDistance = Mathf.Pow(Unit.WeaponComponent.Weapon.DetectionDistance(), 2);
            _delay = Unit.Stats.StayDelay;

            Unit.Animator.OnRun.Execute(0f);
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
                if (_delay > 0f)
                {
                    _delay -= Time.deltaTime;
                }
                else
                {
                    EnterState<UnitStatePatrol>();
                }
            }
        }
        public void Exit() { }
        
        private float DistanceToTarget() => (_levelModel.Character.Position - Unit.Position).sqrMagnitude;
        private bool CanPursuit() => DistanceToTarget() < _detectionDistance;
        private bool IsDeath() => Unit.Health.IsAlive == false;

    }
}