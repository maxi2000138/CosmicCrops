using System;
using _Project.Scripts.Game.Features.AI.Services;
using _Project.Scripts.Game.Features.Units.Enemy.Actions;
using _Project.Scripts.Game.Features.Units.Enemy.Components;
using _Project.Scripts.Game.Features.Units.Enemy.StateMachine.States;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Infrastructure.Time;
using _Project.Scripts.Utils.Extensions;
using VContainer;

namespace _Project.Scripts.Game.Features.AI.Systems
{
  public class EnemyAISystem : TimerSystemComponent<EnemyComponent>
  {
    private const float ExecuteIntervalSeconds = 0.25f;

    private IArtificialIntelligence _artificialIntelligence;
    
    [Inject]
    private void Construct(IArtificialIntelligence artificialIntelligence, ITimeService time)
    {
      _artificialIntelligence = artificialIntelligence;
      Initialize(ExecuteIntervalSeconds, time);
    }

    protected override void OnTimerUpdate()
    {
      Components.Foreach(UpdateUnitBehaviour);
    }
    
    private void UpdateUnitBehaviour(EnemyComponent enemy)
    {
      if (enemy.StateMachine.UnitStateMachine == null) return;
      
      UnitAction bestAction = _artificialIntelligence.MakeBestDecision(enemy);
      
      UpdateUnitState(enemy, enemy.StateMachine.UnitStateMachine, bestAction);
    }

    private void UpdateUnitState(EnemyComponent enemy, IUnitStateMachine stateMachine, UnitAction action)
    {
      if(stateMachine.CurrentState is EnemyStateDeath) return;
      
      switch(action.ActionType)
      {
        case UnitActionType.Patrol:
          if (stateMachine.CurrentState is not EnemyStatePatrol && stateMachine.CurrentState is not EnemyStateIdle)
          {
            stateMachine.Enter<EnemyStatePatrol>();
          }
          break;
        case UnitActionType.Pursuit:
          if (stateMachine.CurrentState is not EnemyStatePursuit || enemy.Target != action.Unit)
          {
            enemy.SetTarget(action.Unit);
            stateMachine.Enter<EnemyStatePursuit>();
          }
          break;
        case UnitActionType.Fight:
          if (stateMachine.CurrentState is not EnemyStateFight || enemy.Target != action.Unit)
          {
            enemy.SetTarget(action.Unit);
            stateMachine.Enter<EnemyStateFight>();
          }
          break;
      }
    }
  }
}