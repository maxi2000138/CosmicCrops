using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Utils.Extensions;

namespace _Project.Scripts.Game.Entities.Character.StateMachine.States
{
    public sealed class CharacterStateNone : CharacterState, IState
    {
        public CharacterStateNone(IStateMachine stateMachine, CharacterComponent character) : base(stateMachine, character)
        {
        }

        void IState.Enter()
        {
            Character.UnitAnimator.OnRun.Execute(0f);
            Character.CleanSubscribe();
        }

        void IState.Exit() { }

        void IState.Tick() { }
    }
}