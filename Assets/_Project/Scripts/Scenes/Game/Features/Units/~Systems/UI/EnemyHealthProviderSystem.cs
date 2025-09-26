using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Units._Components.UI;
using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.Systems;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.Features.Units._Systems.UI
{
  public class EnemyHealthProviderSystem : SystemComponent<EnemyHealthProviderComponent>
  {
    private IUIFactory _uiFactory;
    private LevelModel _levelModel;

    [Inject]
    private void Construct(IUIFactory uiFactory, LevelModel levelModel)
    {
      _uiFactory = uiFactory;
      _levelModel = levelModel;
    }

    protected override void OnEnableComponent(EnemyHealthProviderComponent armament)
    {
      base.OnEnableComponent(armament);

      CreateEnemyHealths(armament).Forget();
    }

    private async UniTaskVoid CreateEnemyHealths(EnemyHealthProviderComponent component)
    {
      foreach (IEnemy enemy in _levelModel.Enemies)
      {
        EnemyHealthViewComponent enemyHealthView = await _uiFactory.CreateEnemyHealth(enemy, component.transform);
      }
    }
  }
}