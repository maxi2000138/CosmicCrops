using _Project.Scripts.Game.Entities._Components;
using _Project.Scripts.Game.Entities._Systems;
using _Project.Scripts.Game.Entities._Systems.UI;
using _Project.Scripts.Game.Entities.Character.Systems;
using _Project.Scripts.Game.Entities.Unit.Systems;
using _Project.Scripts.Game.Features.Collector.Systems;
using _Project.Scripts.Game.Features.Input.Systems;
using _Project.Scripts.Game.Features.Loot.Systems;
using _Project.Scripts.Game.Features.Weapon.Systems;
using _Project.Scripts.Game.UI.Haptic.Systems;
using _Project.Scripts.Infrastructure.Systems;
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
               
                new ExecuteWeaponAmmunitionSystem(),
                
                //View
                new AnimatorSystem(),
                new CollectingViewSystem(),
                new HapticButtonSystem(),
                
                new EnemyHealthViewUpdateSystem(),
                new EnemyHealthProviderSystem(),
            };

            return systems;
        }
    }
}