using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Game.Level.Components;
using _Project.Scripts.Game.Units.Character.Components;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.Logger;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Units.Character.StateMachine.States
{
    public sealed class CharacterStateIdle : CharacterState, IState
    {
        private IJoystickService _joystickService;

        public CharacterStateIdle(IStateMachine stateMachine, CharacterComponent character) : base(stateMachine, character)
        {
            
        }
        
        [Inject]
        private void Construct(IJoystickService joystickService)
        {
            _joystickService = joystickService;
        }

        void IState.Enter()
        {
            DebugLogger.Log("Enter Character Idle State", LogsType.Character);
        }

        void IState.Exit() { }

        void IState.Tick()
        {
            UseGravity();
            
            if (HasInput())
            {
                EnterState<CharacterStateRun>();
            }
        }

        private void UseGravity()
        {
            if (Character.CharacterController.IsGrounded) return;
            
            Vector3 move = Vector3.zero;
            move.y = Physics.gravity.y;
            Character.CharacterController.CharacterController.Move(move * Character.CharacterController.Speed * Time.deltaTime);
        }

        private bool HasInput()
        {
            return _joystickService.GetAxis().sqrMagnitude > _joystickService.GetDeadZone();
        }
    }
}