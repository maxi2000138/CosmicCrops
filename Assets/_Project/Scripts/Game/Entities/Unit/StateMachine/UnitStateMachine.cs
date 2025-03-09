using System;
using System.Collections.Generic;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Entities.Unit.StateMachine.States;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Unit.StateMachine
{
  public class UnitStateMachine : Infrastructure.StateMachine.StateMachine
  {
    public UnitStateMachine(UnitComponent unit)
    {
      States = new Dictionary<Type, IState>
      {
        {typeof(UnitStateNone), new UnitStateNone(this, unit)},

        {typeof(UnitStateIdle), new UnitStateIdle(this, unit)},
        {typeof(UnitStateDeath), new UnitStateDeath(this, unit)},
        {typeof(UnitStateFight), new UnitStateFight(this, unit)},
        {typeof(UnitStatePatrol), new UnitStatePatrol(this, unit)},
        {typeof(UnitStatePursuit), new UnitStatePursuit(this, unit)},
      };
    }
  }
}