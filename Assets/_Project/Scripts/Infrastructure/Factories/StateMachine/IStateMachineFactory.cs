using _Project.Scripts.Infrastructure.StateMachine.Machine;

namespace _Project.Scripts.Infrastructure.Factories.StateMachine
{
    public interface IStateMachineFactory
    {
        IGameStateMachine CreateGameStateMachine(string startScene);
    }
}