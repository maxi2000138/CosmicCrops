﻿using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Utils.Extensions;
using DG.Tweening;
using VContainer;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine.States
{
    public sealed class UnitStateDeath : UnitState, IState
    {
        private LevelModel _levelModel;

        public UnitStateDeath(IStateMachine stateMachine, UnitComponent unit) : base(stateMachine, unit)
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
            Unit.Animator.OnDeath.Execute(R3.Unit.Default);
            Unit.CleanSubscribe();

            DeactivateUnit();

            _levelModel.RemoveEnemy(Unit);
        }

        public void Tick() { }
        public void Exit() { }


        private void DeactivateUnit()
        {
            DOVirtual.DelayedCall(1.5f, () => Unit.Remove()).SetLink(Unit.gameObject);
        }
    }
}