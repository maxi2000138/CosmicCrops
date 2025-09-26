using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Menu.Features.CharacterPreview.Model;
using _Project.Scripts.Menu.Features.Shop.Components;
using DG.Tweening;
using R3;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Menu.Features.Shop.Systems
{
  public class ShopCharacterRendererSystem : SystemComponent<ShopCharacterRendererComponent>
  {
    private CharacterPreviewModel _characterPreviewModel;
        
    [Inject]
    private void Construct(CharacterPreviewModel characterPreviewModel)
    {
      _characterPreviewModel = characterPreviewModel;
    }
        
    protected override void OnEnableComponent(ShopCharacterRendererComponent component)
    {
      base.OnEnableComponent(component);
            
      SetRenderTexture(component);

      component.OnTouch
        .Subscribe(eventData =>
        {
          Vector3 delta = new Vector3(0f, -eventData.delta.x, 0f);
            
          _characterPreviewModel.CharacterPreview.CharacterPreviewModel.transform.localEulerAngles += delta;
        })
        .AddTo(component.LifetimeDisposable);
            
      component.OnStartTouch
        .Subscribe(_ =>
        {
          component.Tween?.Kill();
        })
        .AddTo(component.LifetimeDisposable);
            
      component.OnEndTouch
        .Subscribe(_ =>
        {
          component.Tween = _characterPreviewModel.CharacterPreview.CharacterPreviewModel.transform
            .DOLocalRotateQuaternion(Quaternion.identity, 1f);
        })
        .AddTo(component.LifetimeDisposable);
    }

    protected override void OnDisableComponent(ShopCharacterRendererComponent component)
    {
      base.OnDisableComponent(component);
            
      component.Tween?.Kill();
    }

    private void SetRenderTexture(ShopCharacterRendererComponent component)
    {
      component.RawImage.texture = _characterPreviewModel.RenderTexture;
      component.RawImage.enabled = true;
    }
  }
}