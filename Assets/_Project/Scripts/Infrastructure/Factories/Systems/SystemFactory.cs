using _Project.Scripts.Game.Collector.Systems;
using _Project.Scripts.Game.Infrastructure.StateMachine.Systems;
using _Project.Scripts.Game.Input.Systems;
using _Project.Scripts.Game.Units._Systems;
using _Project.Scripts.Game.Units.Character.Systems;
using _Project.Scripts.Game.Units.Loot.Systems;
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