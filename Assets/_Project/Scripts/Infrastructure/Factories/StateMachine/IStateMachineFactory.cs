using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Game.Level.Components;
using _Project.Scripts.Game.Units.Character.Components;
using _Project.Scripts.Infrastructure.StateMachine;

namespace _Project.Scripts.Infrastructure.Factories.StateMachine
{
    public interface IStateMachineFactory
    {
        IGameStateMachine CreateGameStateMachine();
        IStateMachine CreateCharacterStateMachine(CharacterComponent character);
    }
}