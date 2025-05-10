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

    protected override void OnEnableComponent(CharacterHealthViewComponent armament)
    {
      base.OnEnableComponent(armament);
      
      _levelModel.Character.Health.CurrentHealth
        .Subscribe(SetHealth)
        .AddTo(armament.LifetimeDisposable);
      
      void SetHealth(int health)
      {
        armament.Tween?.Kill();
        armament.Text.text = _levelModel.Character.Health.ToString();

        float fillAmount = Mathematics.Remap(0, _levelModel.Character.Health.MaxHealth, 0, 1, health);
                    
        armament.Fill.fillAmount = fillAmount;
        armament.Tween = armament.FillLerp.DOFillAmount(fillAmount, 0.25f).SetEase(Ease.Linear);
      }
    }

    protected override void OnDisableComponent(CharacterHealthViewComponent component)
    {
      base.OnDisableComponent(component);
            
      component.Tween?.Kill();
    }
  }
}