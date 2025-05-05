namespace _Project.Scripts.Game.Infrastructure.StateMachine
{
    public interface IUnitStateMachine
    {
        void Enter<T>() where T : class, IUnitState;
        void Tick();
    }
}