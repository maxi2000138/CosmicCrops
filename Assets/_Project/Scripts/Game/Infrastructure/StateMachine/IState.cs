namespace _Project.Scripts.Game.Infrastructure.StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Tick();
    }
}