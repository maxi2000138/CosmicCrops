using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine;

namespace _Project.Scripts.Infrastructure.Factories.StateMachine
{
    public interface IStateMachineFactory
    {
        IGameStateMachine CreateGameStateMachine();
        IStateMachine CreateCharacterStateMachine(CharacterComponent character);
        IStateMachine CreateUnitStateMachine(UnitComponent unit);
    }
}