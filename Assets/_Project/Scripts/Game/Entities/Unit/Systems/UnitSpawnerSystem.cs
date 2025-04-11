using _Project.Scripts.Game.Entities.Unit._Configs;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Data;
using _Project.Scripts.Game.Features.Weapon.Factories;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Factories.StateMachine;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.Entities.Unit.Systems
{
  public class UnitSpawnerSystem : SystemComponent<UnitSpawnerComponent>
  {
    private IStateMachineFactory _stateMachineFactory;
    private IWeaponFactory _weaponFactory;
    private IGameFactory _gameFactory;
    private UnitsConfig _unitsConfig;

    [Inject]
    private void Construct(IGameFactory gameFactory, IStateMachineFactory stateMachineFactory, UnitsConfig unitsConfig, 
      IWeaponFactory weaponFactory)
    {
      _weaponFactory = weaponFactory;
      _stateMachineFactory = stateMachineFactory;
      _unitsConfig = unitsConfig;
      _gameFactory = gameFactory;
    }
    
    protected override void OnEnableComponent(UnitSpawnerComponent component)
    {
      base.OnEnableComponent(component);
      
      CreateUnit(component).Forget();
    }
    
    private async UniTaskVoid CreateUnit(UnitSpawnerComponent spawner)
    {
      UnitData unitData = _unitsConfig.Data[spawner.Unit];
      UnitComponent unit = await _gameFactory.CreateUnit(spawner.Unit, spawner.Position, spawner.transform.parent);
      
      WeaponComponent weapon = await _weaponFactory.CreateCharacterWeapon(WeaponType.Knife, unit.WeaponMediator.Container);
      unit.WeaponMediator.SetWeapon(weapon);

      unit.Stats = unitData;

      unit.Health.SetBaseHealth(unitData.Health);
      unit.Health.SetMaxHealth(unitData.Health);
      unit.Health.CurrentHealth.SetValueAndForceNotify(unitData.Health);
      
      unit.StateMachine.CreateStateMachine(_stateMachineFactory.CreateUnitStateMachine(unit));
    }
  }
}