using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Game.Level.Model;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.Logger;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Entities.Character.StateMachine.States
{
    public sealed class CharacterStateIdle : CharacterState, IState
    {
        private IJoystickService _joystickService;
        private LevelModel _levelModel;

        public CharacterStateIdle(IStateMachine stateMachine, CharacterComponent character) : base(stateMachine, character)
        {
            
        }
        
        [Inject]
        private void Construct(IJoystickService joystickService, LevelModel levelModel)
        {
            _levelModel = levelModel;
            _joystickService = joystickService;
        }

        void IState.Enter()
        {
            Character.Animator.OnRun.Execute(0f);

            DebugLogger.Log("Enter Character Idle State", LogsType.Character);
        }

        void IState.Exit() { }

        void IState.Tick()
        {
            UseGravity();
            
            if (HasInput())
            {
                EnterState<CharacterStateRun>();
                return;
            }

            if (HasDetectedLoot())
            {
                EnterState<CharacterStateLoot>();
                return;
            }
            
            if (HasDetectedTarget())
            {
                EnterState<CharacterStateFight>();
            }
        }

        private void UseGravity()
        {
            if (Character.CharacterController.IsGrounded) return;
            
            Vector3 move = Vector3.zero;
            move.y = Physics.gravity.y;
            Character.CharacterController.CharacterController.Move(move * Character.CharacterController.Speed * Time.deltaTime);
        }
        
        private bool HasDetectedLoot()
        {
            for (int i = 0; i < _levelModel.Loot.Count; i++)
            {
                //TODO: Set distance from config
                if (DistanceToTarget(_levelModel.Loot[i].Position) < 5f)
                {
                    return true;
                }
            }

            return false;
        }
        
        private bool HasDetectedTarget()
        {
            for (int i = 0; i < _levelModel.Enemies.Count; i++)
            {
                if (DistanceToTarget(_levelModel.Enemies[i].Position) < Character.WeaponComponent.Weapon.AttackDistance())
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasInput()
        {
            return _joystickService.GetAxis().sqrMagnitude > _joystickService.GetDeadZone();
        }
        
        
        private float DistanceToTarget(Vector3 target) => (Character.Position - target).sqrMagnitude;
    }

}