using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Enemy.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine;

namespace _Project.Scripts.Infrastructure.Factories.StateMachine
{
    public interface IStateMachineFactory
    {
        IUnitStateMachine CreateCharacterStateMachine(CharacterComponent character);
        IUnitStateMachine CreateEnemyStateMachine(EnemyComponent enemy);
    }
}