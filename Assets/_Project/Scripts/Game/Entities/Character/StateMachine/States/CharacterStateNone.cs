using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Utils.Extensions;

namespace _Project.Scripts.Game.Entities.Character.StateMachine.States
{
    public sealed class CharacterStateNone : CharacterState, IUnitState
    {
        public CharacterStateNone(IUnitStateMachine unitStateMachine, CharacterComponent character) : base(unitStateMachine, character)
        {
        }

        void IUnitState.Enter()
        {
            Character.UnitAnimator.OnRun.Execute(0f);
            Character.Radar.Clear.Execute(R3.Unit.Default);
            Character.CleanSubscribe();
        }

        void IUnitState.Exit() { }

        void IUnitState.Tick() { }
    }
}