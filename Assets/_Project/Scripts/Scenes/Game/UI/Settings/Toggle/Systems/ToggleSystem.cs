using _Project.Scripts.Game.UI.Settings.Toggle.Components;
using _Project.Scripts.Infrastructure.Systems;
using DG.Tweening;
using R3;
using UnityEngine;

namespace _Project.Scripts.Game.UI.Settings.Toggle.Systems
{
    public sealed class ToggleSystem : SystemComponent<ToggleComponent>
    {
        protected override void OnEnableComponent(ToggleComponent component)
        {
            base.OnEnableComponent(component);

            component.Handle.OnDrag
                .Subscribe(data => HandleDrag(component, data.delta))
                .AddTo(component.LifetimeDisposable);

            component.Handle.OnEndDrag
                .Subscribe(_ => component.IsEnable.Value = component.Handle.X > 0f)
                .AddTo(component.LifetimeDisposable);

            component.OnClick
                .Subscribe(_ => component.IsEnable.Value = !component.IsEnable.Value)
                .AddTo(component.LifetimeDisposable);

            component.IsEnable
                .Subscribe(isEnable => SetActive(component, isEnable))
                .AddTo(component.LifetimeDisposable);
        }

        private void SetActive(ToggleComponent component, bool isActive)
        {
            float posX = component.Offset * (isActive ? 1f : -1f);
            
            component.Tween?.Kill();
            
            component.Tween = component.Handle.transform
                .DOLocalMoveX(posX, 0.1f)
                .SetEase(Ease.Linear)
                .SetLink(component.gameObject);
            
            UpdateVisual(component, isActive);
        }

        private void HandleDrag(ToggleComponent component, Vector2 delta)
        {
            Vector3 initPos = component.Handle.transform.localPosition;
            float xPos = Mathf.Clamp(initPos.x + delta.x, -component.Offset, component.Offset);
            component.Handle.transform.localPosition = new Vector3(xPos, initPos.y, initPos.z);
            
            UpdateVisual(component, component.Handle.X > 0f);
        }

        private void UpdateVisual(ToggleComponent component, bool isActive)
        {
            component.IsActive(isActive);
            component.Handle.IsActive(isActive);
        }
    }
}