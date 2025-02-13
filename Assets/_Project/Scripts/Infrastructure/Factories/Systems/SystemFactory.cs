using _Project.Scripts.Game.Collector.Systems;
using _Project.Scripts.Game.Entities._Systems;
using _Project.Scripts.Game.Entities.Character.Systems;
using _Project.Scripts.Game.Entities.Loot.Systems;
using _Project.Scripts.Game.Entities.Unit.Systems;
using _Project.Scripts.Game.Input.Systems;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.UI.Haptic.Systems;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.Factories.Systems
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class SystemFactory : ISystemFactory
    {
        ISystem[] ISystemFactory.CreateGameSystems()
        {
            ISystem[] systems = 
            {
                new CharacterSpawnerSystem(),
                new StateMachineUpdateSystem(),
                
                new JoystickUpdateSystem(),
                
                new LootSpawnerSystem(),
                new UnitSpawnerSystem(),
                
                new ExecuteCollectorSystem(),
                
                //View
                new AnimatorSystem(),
                new CollectingViewSystem(),
                new HapticButtonSystem(),
            };

            return systems;
        }
    }
}