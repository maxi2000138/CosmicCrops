using _Project.Scripts._Infrastructure.ComponentSystemsCore.Systems;

namespace _Project.Scripts._Infrastructure.Factories.Systems
{
    public interface ISystemFactory
    {
        ISystem[] CreateGameSystems();
    }
}