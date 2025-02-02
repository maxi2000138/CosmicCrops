using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Game.Level.Components;
using _Project.Scripts.Game.Units.Character.Components;
using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Game.Units.Character.StateMachine.States
{
    public sealed class CharacterStateNone : CharacterState, IState
    {
        public CharacterStateNone(IStateMachine stateMachine, CharacterComponent character) : base(stateMachine, character)
        {
        }

        void IState.Enter()
        {
            Character.Animator.OnRun.Execute(0f);
            Character.CleanSubscribe();
        }

        void IState.Exit() { }

        void IState.Tick() { }
    }
}