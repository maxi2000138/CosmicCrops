using _Project.Scripts.Game.Entities._Components.UI;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils;
using DG.Tweening;
using VContainer;
using R3;

namespace _Project.Scripts.Game.Entities._Systems.UI
{
  public class CharacterHealthViewUpdateSystem : SystemComponent<CharacterHealthViewComponent>
  {
    private LevelModel _levelModel;

    [Inject]
    private void Construct(LevelModel levelModel)
    {
      _levelModel = levelModel;
    }

    protected override void OnEnableComponent(CharacterHealthViewComponent component)
    {
      base.OnEnableComponent(component);
      
      _levelModel.Character.Health.CurrentHealth
        .Subscribe(SetHealth)
        .AddTo(component.LifetimeDisposable);
      
      void SetHealth(int health)
      {
        component.Tween?.Kill();
        component.Text.text = _levelModel.Character.Health.ToString();

        float fillAmount = Mathematics.Remap(0, _levelModel.Character.Health.MaxHealth, 0, 1, health);
                    
        component.Fill.fillAmount = fillAmount;
        component.Tween = component.FillLerp.DOFillAmount(fillAmount, 0.25f).SetEase(Ease.Linear);
      }
    }

    protected override void OnDisableComponent(CharacterHealthViewComponent component)
    {
      base.OnDisableComponent(component);
            
      component.Tween?.Kill();
    }
  }
}