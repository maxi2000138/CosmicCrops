using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.Logger;
using _Project.Scripts.Utils;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Entities.Character.StateMachine.States
{
    public sealed class CharacterStateRun : CharacterState, IState
    {
        private IJoystickService _joystickService;
        private ICameraService _cameraService;

        private const float RayDistance = 5f;
        private const float LerpRotate = 0.25f;
        
        private float _angle;

        public CharacterStateRun(IStateMachine stateMachine, CharacterComponent character) : base(stateMachine, character) { }
        
        [Inject]
        private void Construct(IJoystickService joystickService, ICameraService cameraService)
        {
            _joystickService = joystickService;
            _cameraService = cameraService;
        }

        void IState.Enter()
        {
            Character.UnitAnimator.OnRun.Execute(1f);
            
            DebugLogger.Log("Enter Character Run State", LogsType.Character);
        }

        void IState.Exit() { }

        void IState.Tick()
        {
            if (HasNoInput())
            {
                EnterState<CharacterStateIdle>();
                
                return;
            }

            Move();
            Rotate();
        }

        private bool HasNoInput() => _joystickService.GetAxis().sqrMagnitude < _joystickService.GetDeadZone();

        private void Move()
        {
            _angle = Mathf.Atan2(_joystickService.GetAxis().x, _joystickService.GetAxis().y) * 
                Mathf.Rad2Deg + _cameraService.Camera.transform.eulerAngles.y; 

            Vector3 move = Quaternion.Euler(0f, _angle, 0f) * Vector3.forward;

            Vector3 next = Character.Position + move * Character.CharacterController.Speed * Time.deltaTime;
                        
            Ray ray = new Ray { origin = next, direction = Vector3.down };

            if (!Physics.Raycast(ray, RayDistance, Layers.Ground))
            {
                Debug.DrawLine(next, next + Vector3.down * RayDistance, Color.red);
                return;
            }
                
            move.y = Character.CharacterController.IsGrounded ? 0f : Physics.gravity.y;

            Character.CharacterController.CharacterController.Move(move * Character.CharacterController.Speed * Time.deltaTime);
        }

        private void Rotate()
        {
            float lerpAngle = Mathf.LerpAngle(Character.CharacterController.Angle, _angle, LerpRotate);
            Character.CharacterController.transform.rotation = Quaternion.Euler(0f, lerpAngle, 0f);
        }
    }


}