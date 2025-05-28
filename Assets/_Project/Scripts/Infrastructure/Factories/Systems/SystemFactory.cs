using _Project.Scripts.Game.Entities._Systems;
using _Project.Scripts.Game.Entities._Systems.UI;
using _Project.Scripts.Game.Entities.Character.Systems;
using _Project.Scripts.Game.Entities.Enemy.Systems;
using _Project.Scripts.Game.Features.Abilities.Systems;
using _Project.Scripts.Game.Features.AI.Systems;
using _Project.Scripts.Game.Features.Collector.Systems;
using _Project.Scripts.Game.Features.Input.Systems;
using _Project.Scripts.Game.Features.Level.Systems;
using _Project.Scripts.Game.Features.Loot.Systems;
using _Project.Scripts.Game.Features.Weapon.Systems;
using _Project.Scripts.Game.UI.Haptic.Systems;
using _Project.Scripts.Game.UI.PointerArrow.Systems;
using _Project.Scripts.Game.UI.Radar.Systems;
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
                new LootSpawnerSystem(),
                new EnemySpawnerSystem(),
                
                new StateMachineUpdateSystem(),
                new EnemyAISystem(),
                
                new JoystickUpdateSystem(),
                
                new BuildGroundNavMeshSystem(),
                
                new ExecuteCollectorSystem(),
               
                new ExecuteWeaponAmmunitionSystem(),
                new ArmamentLifetimeSystem(),
                new DirectionArmamentMoveSystem(),
                new HomingArmamentMoveSystem(),
                new ThrowableArmamentMoveSystem(),
                new ArmamentCollisionSystem(),
                
                new ProcessAbilitySystem(),
                
                //View
                new AnimatorSystem(),
                new CollectingViewSystem(),
                new HapticButtonSystem(),
                
                new RadarDrawSystem(),
                new PointerArrowProviderSystem(),
                new PointerArrowUpdateSystem(),
                
                new CharacterHealthViewUpdateSystem(),
                new EnemyHealthViewUpdateSystem(),
                new EnemyHealthProviderSystem(),
            };

            return systems;
        }
    }
}