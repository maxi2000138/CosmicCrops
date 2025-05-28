using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Input;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Entities.Character.StateMachine.States
{
  public class CharacterStateFight : CharacterState, IUnitState
  {
    private IJoystickService _joystickService;
    private LevelModel _levelModel;

    [Inject]
    private void Construct(IJoystickService joystickService, LevelModel levelModel)
    {
      _joystickService = joystickService;
      _levelModel = levelModel;
    }
    
    public CharacterStateFight(IUnitStateMachine unitStateMachine, CharacterComponent character) : base(unitStateMachine, character)
    {
            
    }
    
    void IUnitState.Enter()
    {
      Character.Radar.Clear.Execute(R3.Unit.Default);

      SetTarget(null);
    }
    
    void IUnitState.Exit()
    {
      SetTarget(null);
    }
    
    void IUnitState.Tick()
    {
      UseGravity();
            
      if (HasInput())
      {
        EnterState<CharacterStateRun>();
                
        return;
      }

      if (TrySetTarget())
      {
        LockAtTarget();

        if (CanAttack())
        {
          Attack();
        }
      }
      else
      {
        EnterState<CharacterStateIdle>();
      }
    }
    
    private void Attack()
    {
      Character.UnitAnimator.OnAttack.Execute(Character.WeaponMediator.CurrentWeapon.Weapon.AttackInterval());
      Character.WeaponMediator.CurrentWeapon.Weapon.Attack(Character.Target);
    }
    
    private bool HasInput()
    {
      return _joystickService.GetAxis().sqrMagnitude > _joystickService.GetDeadZone();
    }
    
    private void LockAtTarget()
    {
      Quaternion lookRotation = Quaternion.LookRotation(Character.Target.Position - Character.Position);

      Character.CharacterController.transform.rotation = Quaternion
        .Slerp(Character.CharacterController.transform.rotation, lookRotation, Character.WeaponMediator.CurrentWeapon.Weapon.AimingSpeed());
    }

    private void UseGravity()
    {
      if (Character.CharacterController.IsGrounded) return;
            
      Vector3 move = Vector3.zero;
      move.y = Physics.gravity.y;
      Character.CharacterController.CharacterController.Move(move * Character.CharacterController.Speed * Time.deltaTime);
    }
    
    private bool TrySetTarget()
    {
      if (_levelModel.Enemies.Count == 0)
      {
        return false;
      }

      int index = FindNearestTargetIndex();

      if (index >= 0)
      {
        SetTarget(_levelModel.Enemies[index]);

        return true;
      }

      return false;
    }
    
    private int FindNearestTargetIndex()
    {
      int index = -1;
            
      float minDistance = Character.WeaponMediator.CurrentWeapon.Weapon.AttackDistance();

      for (int i = 0; i < _levelModel.Enemies.Count; i++)
      {
        float distance = DistanceToTarget(_levelModel.Enemies[i].Position);

        if (distance < Character.WeaponMediator.CurrentWeapon.Weapon.AttackDistance())
        {
          if (distance < minDistance)
          {
            index = i;
            minDistance = distance;
          }
        }
      }

      return index;
    }
    
    private float DistanceToTarget(Vector3 target) => (Character.Position - target).magnitude;


    
    private bool HasFacingTarget()
    {
      float angle = Vector3.Angle(Character.Forward.normalized, (Character.Target.Position - Character.Position).normalized);

      return angle < 5f;
    }
    
    private bool CanAttack()
    {
      return HasFacingTarget() && Character.WeaponMediator.CurrentWeapon.Weapon.CanAttack();
    }

    private void SetTarget(IUnit unit)
    {
      Character.SetTarget(unit);
      _levelModel.Target.Value = unit;
    }
  }

}