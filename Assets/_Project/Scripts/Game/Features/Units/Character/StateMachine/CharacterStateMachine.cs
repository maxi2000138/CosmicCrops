﻿using System;
using System.Collections.Generic;
using _Project.Scripts.Game.Features.Units.Character.Components;
using _Project.Scripts.Game.Features.Units.Character.StateMachine.States;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Features.Units.Character.StateMachine
{
    public sealed class CharacterStateMachine : UnitStateMachine
    {
        public CharacterStateMachine(CharacterComponent character)
        {
            States = new Dictionary<Type, IUnitState>
            {
                {typeof(CharacterStateNone), new CharacterStateNone(this, character)},

                {typeof(CharacterStateIdle), new CharacterStateIdle(this, character)},
                {typeof(CharacterStateRun), new CharacterStateRun(this, character)},
                {typeof(CharacterStateLoot), new CharacterStateLoot(this, character)},
                {typeof(CharacterStateFight), new CharacterStateFight(this, character)},
                {typeof(CharacterStateDeath), new CharacterStateDeath(this, character)},
            };
        }
    }
}