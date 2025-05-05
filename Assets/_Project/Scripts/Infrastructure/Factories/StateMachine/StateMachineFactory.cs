using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Character.StateMachine;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Entities.Unit.StateMachine;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Utils.Extensions;
using VContainer;
using VContainer.Unity;
using IState = _Project.Scripts.Infrastructure.StateMachine.States.Interfaces.IState;
using UnitStateMachine = _Project.Scripts.Game.Entities.Unit.StateMachine.UnitStateMachine;

namespace _Project.Scripts.Infrastructure.Factories.StateMachine
{
    public sealed class StateMachineFactory : IStateMachineFactory
    {
        private readonly LifetimeScope _lifetimeScope;

        public StateMachineFactory(LifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public IUnitStateMachine CreateCharacterStateMachine(CharacterComponent character)
        {
            CharacterStateMachine characterStateMachine = new CharacterStateMachine(character);
            characterStateMachine.States.Values.Foreach(_lifetimeScope.Container.Inject);
            return characterStateMachine;
        }
        
        public IUnitStateMachine CreateUnitStateMachine(UnitComponent unit)
        {
            UnitStateMachine unitStateMachine = new UnitStateMachine(unit);
            unitStateMachine.States.Values.Foreach(_lifetimeScope.Container.Inject);
            return unitStateMachine;
        }
    }
}