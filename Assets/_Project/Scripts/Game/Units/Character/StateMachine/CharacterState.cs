using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Game.Level.Components;
using _Project.Scripts.Game.Units.Character.Components;

namespace _Project.Scripts.Game.Units.Character.StateMachine
{
    public abstract class CharacterState
    {
        private readonly IStateMachine _stateMachine;
        
        protected CharacterComponent Character { get; }

        protected CharacterState(IStateMachine stateMachine, CharacterComponent character)
        {
            _stateMachine = stateMachine;
            Character = character;
        }

        protected void EnterState<T>() where T : CharacterState, IState => _stateMachine.Enter<T>();
    }
}