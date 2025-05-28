using System;
using System.Collections.Generic;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Entities.Unit.StateMachine.States;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine
{
  public class EnemyStateMachine : UnitStateMachine
  {
    public EnemyStateMachine(EnemyComponent enemy)
    {
      States = new Dictionary<Type, IUnitState>
      {
        {typeof(EnemyStateNone), new EnemyStateNone(this, enemy)},

        {typeof(EnemyStateIdle), new EnemyStateIdle(this, enemy)},
        {typeof(EnemyStateDeath), new EnemyStateDeath(this, enemy)},
        {typeof(EnemyStateFight), new EnemyStateFight(this, enemy)},
        {typeof(EnemyStatePatrol), new EnemyStatePatrol(this, enemy)},
        {typeof(EnemyStatePursuit), new EnemyStatePursuit(this, enemy)},
      };
    }
  }
}