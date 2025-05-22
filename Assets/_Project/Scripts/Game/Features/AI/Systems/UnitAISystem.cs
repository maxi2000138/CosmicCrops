using System;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Entities.Unit.StateMachine.States;
using _Project.Scripts.Game.Features.UtilityAI;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using VContainer;

namespace _Project.Scripts.Game.Features.AI.Systems
{
  public class UnitAISystem : SystemComponent<UnitComponent>
  {
    private IArtificialIntelligence _artificialIntelligence;

    [Inject]
    private void Construct(IArtificialIntelligence artificialIntelligence)
    {
      _artificialIntelligence = artificialIntelligence;
    }
    
    protected override void OnUpdate()
    {
      base.OnUpdate();
      
      Components.Foreach(UpdateUnitBehaviour);
    }
    
    private void UpdateUnitBehaviour(UnitComponent unit)
    {
      if (unit.StateMachine.UnitStateMachine == null) return;
      
      UnitAction bestAction = _artificialIntelligence.MakeBestDecision(unit);
      
      UpdateUnitState(unit, unit.StateMachine.UnitStateMachine, bestAction);
    }

    private void UpdateUnitState(UnitComponent unit, IUnitStateMachine stateMachine, UnitAction action)
    {
      switch(action.ActionType)
      {
        case UnitActionType.Idle:
          stateMachine.Enter<UnitStateIdle>();
          break;
        case UnitActionType.Patrol:
          stateMachine.Enter<UnitStatePatrol>();
          break;
        case UnitActionType.Pursuit:
          unit.SetTarget(action.Target);
          stateMachine.Enter<UnitStatePursuit>();
          break;
        case UnitActionType.Fight:
          stateMachine.Enter<UnitStateFight>();
          break;
      }
    }
  }
}