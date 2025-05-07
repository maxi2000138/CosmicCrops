using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.Logger;
using _Project.Scripts.Utils;
using _Project.Scripts.Utils.Constants;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Entities.Character.StateMachine.States
{
    public sealed class CharacterStateRun : CharacterState, IUnitState
    {
        private IJoystickService _joystickService;
        private ICameraService _cameraService;

        private const float RayDistance = 5f;
        private const float LerpRotate = 0.25f;
        
        private float _angle;

        public CharacterStateRun(IUnitStateMachine unitStateMachine, CharacterComponent character) : base(unitStateMachine, character) { }
        
        [Inject]
        private void Construct(IJoystickService joystickService, ICameraService cameraService)
        {
            _joystickService = joystickService;
            _cameraService = cameraService;
        }

        void IUnitState.Enter()
        {
            Character.UnitAnimator.OnRun.Execute(1f);
            Character.Radar.Draw.Execute(R3.Unit.Default);
        }

        void IUnitState.Exit() { }

        void IUnitState.Tick()
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