using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;

namespace _Project.Scripts.Game.Entities.Character.StateMachine
{
    public abstract class CharacterState
    {
        private readonly IUnitStateMachine _unitStateMachine;
        
        protected CharacterComponent Character { get; }

        protected CharacterState(IUnitStateMachine unitStateMachine, CharacterComponent character)
        {
            _unitStateMachine = unitStateMachine;
            Character = character;
        }

        protected void EnterState<T>() where T : CharacterState, IUnitState => _unitStateMachine.Enter<T>();
    }
}