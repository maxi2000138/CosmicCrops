using _Project.Scripts.Infrastructure.Systems;

namespace _Project.Scripts.Infrastructure.Factories.Systems
{
    public interface ISystemFactory
    {
        ISystem[] CreateGameSystems();
    }
}