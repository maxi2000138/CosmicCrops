using _Project.Scripts._Infrastructure.ComponentSystemsCore.Systems;
using JetBrains.Annotations;

namespace _Project.Scripts._Infrastructure.Factories.Systems
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class SystemFactory : ISystemFactory
    {
        ISystem[] ISystemFactory.CreateGameSystems()
        {
            ISystem[] systems = 
            {
                
            };

            return systems;
        }
    }
}