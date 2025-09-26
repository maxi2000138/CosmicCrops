namespace _Project.Scripts.Game.Infrastructure.StateMachine
{
    public interface IUnitStateMachine
    {
        IUnitState CurrentState { get; }
        void Enter<T>() where T : class, IUnitState;
        void Tick();
    }
}