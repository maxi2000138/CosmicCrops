using _Project.Scripts.Game.Features.Loot.Components;
using _Project.Scripts.Game.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Systems;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.Features.Loot.Systems
{
  public class LootSpawnerSystem : SystemComponent<LootSpawnerComponent>
  {
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }

    protected override void OnEnableComponent(LootSpawnerComponent armament)
    {
      base.OnEnableComponent(armament);

      CreateLoot(armament).Forget();
    }
    
    private async UniTaskVoid CreateLoot(LootSpawnerComponent component)
    {
      LootComponent loot = await _gameFactory.CreateLoot(component.LootType, component.Position, component.transform.parent);
    }
  }
}