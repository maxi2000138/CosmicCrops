namespace _Project.Scripts.Game.Infrastructure.StateMachine
{
    public interface IUnitState
    {
        void Enter();
        void Exit();
        void Tick();
    }
}