using _Project.Scripts.Game.Features.Units._Components.UI;
using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using R3;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Features.Units._Systems.UI
{
  public class EnemyHealthViewUpdateSystem : SystemComponent<EnemyHealthViewComponent>
  {
    private ICameraService _cameraService;

    [Inject]
    private void Construct(ICameraService cameraService)
    {
      _cameraService = cameraService;
    }

    protected override void OnLateUpdate()
    {
      base.OnLateUpdate();

      Components.Foreach(UpdatePosition);
    }

    protected override void OnEnableComponent(EnemyHealthViewComponent armament)
    {
      base.OnEnableComponent(armament);

      armament.Enemy
        .First(enemy => enemy != null)
        .Subscribe(enemy => SubscribeOnChangeHealth(armament, enemy))
        .AddTo(armament.LifetimeDisposable);
    }

    private void SubscribeOnChangeHealth(EnemyHealthViewComponent healthView, IEnemy enemy)
    {
      enemy.Health.CurrentHealth
        .Subscribe(health => {
          float fillAmount = Mathematics.Remap(0, enemy.Health.MaxHealth, 0, 1, health);

          healthView.Text.text = enemy.Health.ToString();
          healthView.Fill.fillAmount = fillAmount;
        })
        .AddTo(healthView.LifetimeDisposable);
    }

    private void UpdatePosition(EnemyHealthViewComponent component)
    {
      if (component.Enemy.Value.Health.IsAlive == false)
      {
        component.CanvasGroup.alpha = 0f;

        return;
      }

      float height = component.Enemy.Value.Height;
      Vector3 position = component.Enemy.Value.Position.AddY(height);
      Vector3 screenPoint = _cameraService.Camera.WorldToScreenPoint(position);
      Vector3 viewportPoint = _cameraService.Camera.WorldToViewportPoint(position);
      component.transform.position = screenPoint.SetZ(0f);
      component.CanvasGroup.alpha += _cameraService.IsOnScreen(viewportPoint)
        ? Time.deltaTime * 1f : Time.deltaTime * -1f;
    }
  }
}