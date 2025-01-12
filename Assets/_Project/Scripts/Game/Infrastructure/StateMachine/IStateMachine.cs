namespace _Project.Scripts.Game.Infrastructure.StateMachine
{
    public interface IStateMachine
    {
        void Enter<T>() where T : class, IState;
        void Tick();
    }
}