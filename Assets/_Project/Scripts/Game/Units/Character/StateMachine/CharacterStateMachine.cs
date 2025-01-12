using System;
using System.Collections.Generic;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Game.Level.Components;
using _Project.Scripts.Game.Units.Character.Components;
using _Project.Scripts.Game.Units.Character.StateMachine.States;

namespace _Project.Scripts.Game.Units.Character.StateMachine
{
    public sealed class CharacterStateMachine : Infrastructure.StateMachine.StateMachine
    {
        public CharacterStateMachine(CharacterComponent character)
        {
            States = new Dictionary<Type, IState>
            {
                {typeof(CharacterStateNone), new CharacterStateNone(this, character)},

                {typeof(CharacterStateIdle), new CharacterStateIdle(this, character)},
                {typeof(CharacterStateRun), new CharacterStateRun(this, character)},
            };
        }
    }
}