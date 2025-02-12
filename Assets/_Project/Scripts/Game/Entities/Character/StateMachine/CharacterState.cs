using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Character.StateMachine
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