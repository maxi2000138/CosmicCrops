using System;
using System.Collections.Generic;
using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Character.StateMachine.States;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Character.StateMachine
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
                {typeof(CharacterStateLoot), new CharacterStateLoot(this, character)},
            };
        }
    }
}