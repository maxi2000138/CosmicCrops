using _Project.Scripts.Game.Entities._Components.UI;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.Systems;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.Entities._Systems.UI
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

    protected override void OnEnableComponent(EnemyHealthProviderComponent component)
    {
      base.OnEnableComponent(component);

      CreateEnemyHealths(component).Forget();
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