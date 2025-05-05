using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Loot.Interface;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.Logger;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Entities.Character.StateMachine.States
{
  public sealed class CharacterStateLoot : CharacterState, IUnitState
  {
    private IJoystickService _joystickService;
    private LevelModel _levelModel;
    private ILoot _loot;

    public CharacterStateLoot(IUnitStateMachine unitStateMachine, CharacterComponent character) : base(unitStateMachine, character) { }
        
    [Inject]
    private void Construct(IJoystickService joystickService, LevelModel levelModel)
    {
      _levelModel = levelModel;
      _joystickService = joystickService;
    }

    void IUnitState.Enter()
    {
      
    }

    void IUnitState.Exit() { }

    void IUnitState.Tick()
    {
      UseGravity();
      
      if (HasInput())
      {
        if(IsCollecting())
          Character.Collector.Collector.CancelCollecting();
        
        EnterState<CharacterStateRun>();
        return;
      }
        
      if (TrySetLoot())
      {
        if (CanCollect())
        {
          Character.Collector.Collector.StartCollecting(_loot);
        }
      }
      
      if(IsCollecting() == false)
      {
        EnterState<CharacterStateIdle>();
      }
    }

    private void UseGravity()
    {
      if (Character.CharacterController.IsGrounded) return;
            
      Vector3 move = Vector3.zero;
      move.y = Physics.gravity.y;
      Character.CharacterController.CharacterController.Move(move * Character.CharacterController.Speed * Time.deltaTime);
    }

    private bool HasInput() => _joystickService.GetAxis().sqrMagnitude >= _joystickService.GetDeadZone();


    private bool TrySetLoot()
    {
      if (_levelModel.Loot.Count == 0)
      {
        return false;
      }

      int index = FindNearestLootIndex();

      if (index >= 0)
      {
        SetLoot(_levelModel.Loot[index]);

        return true;
      }

      return false;
    }
    
    private bool CanCollect() => Character.Collector.Collector.CanCollect();
    private bool IsCollecting() => Character.Collector.Collector.IsCollecting();


    private int FindNearestLootIndex()
    {
      int index = -1;
            
      float minDistanceValue = Character.Collector.Collector.CollectorDistance;
      float minDistance = minDistanceValue;

      for (int i = 0; i < _levelModel.Loot.Count; i++)
      {
        float distance = DistanceToLoot(_levelModel.Loot[i].Position);

        if (distance < minDistanceValue)
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
    
    private float DistanceToLoot(Vector3 target) => (Character.Position - target).sqrMagnitude;
    
    private void SetLoot(ILoot loot)
    {
      _loot = loot;
      _levelModel.CurrentLoot.Value = _loot;
    }
  }
}