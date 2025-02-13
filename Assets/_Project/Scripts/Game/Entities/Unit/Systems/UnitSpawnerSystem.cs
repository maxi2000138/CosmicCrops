using _Project.Scripts.Game.Entities.Loot.Components;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Systems;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.Entities.Unit.Systems
{
  public class UnitSpawnerSystem : SystemComponent<UnitSpawnerComponent>
  {
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IGameFactory gameFactory)
    {
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
    }
  }
}