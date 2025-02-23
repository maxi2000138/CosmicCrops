using System;
using System.Collections;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Factories.StateMachine;
using _Project.Scripts.Infrastructure.Systems;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.Entities.Unit.Systems
{
  public class UnitSpawnerSystem : SystemComponent<UnitSpawnerComponent>
  {
    private IGameFactory _gameFactory;
    private IStateMachineFactory _stateMachineFactory;

    [Inject]
    private void Construct(IGameFactory gameFactory, IStateMachineFactory stateMachineFactory)
    {
      _stateMachineFactory = stateMachineFactory;
      _gameFactory = gameFactory;
    }
    
    protected override void OnEnableComponent(UnitSpawnerComponent component)
    {
      base.OnEnableComponent(component);
      
      CreateUnit(component).Forget();
    }
    
    private async UniTaskVoid CreateUnit(UnitSpawnerComponent component)
    {
      UnitComponent unit = await _gameFactory.CreateUnit(component.Position, component.transform.parent);

      unit.StateMachine.CreateStateMachine(_stateMachineFactory.CreateUnitStateMachine(unit));
    }
  }
}