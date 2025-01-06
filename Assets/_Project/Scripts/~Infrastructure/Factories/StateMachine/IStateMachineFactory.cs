using _Project.Scripts._Infrastructure.StateMachine.Machine;

namespace _Project.Scripts._Infrastructure.Factories.StateMachine
{
    public interface IStateMachineFactory
    {
        IGameStateMachine CreateGameStateMachine();
    }
}