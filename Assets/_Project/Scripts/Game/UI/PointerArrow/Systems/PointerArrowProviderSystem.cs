using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.UI.PointerArrow.Components;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.Systems;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Game.UI.PointerArrow.Systems
{
  public class PointerArrowProviderSystem : SystemComponent<PointerArrowProviderComponent>
  {
    private IUIFactory _uiFactory;
    private LevelModel _levelModel;

    [Inject]
    private void Construct(IUIFactory uiFactory, LevelModel levelModel)
    {
      _uiFactory = uiFactory;
      _levelModel = levelModel;
    }

    protected override void OnEnableComponent(PointerArrowProviderComponent component)
    {
      base.OnEnableComponent(component);
            
      CreatePointers(component).Forget();
    }

    private async UniTaskVoid CreatePointers(PointerArrowProviderComponent component)
    {
      foreach (IEnemy enemy in _levelModel.Enemies)
      {
        PointerArrowComponent pointerArrow = await _uiFactory.CreatePointerArrow(component.transform);

        pointerArrow.SetTarget(enemy);
        pointerArrow.SetRectProvider(component.Rect);
        pointerArrow.SetOffset(component.Offset);
        pointerArrow.CanvasGroup.alpha = 0f;
      }
    }

  }
}