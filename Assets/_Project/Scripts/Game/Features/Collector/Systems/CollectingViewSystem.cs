using _Project.Scripts.Game.Features.Collector.Components;
using _Project.Scripts.Game.Features.Inventory;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using DG.Tweening;
using R3;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Features.Collector.Systems
{
    public sealed class CollectingViewSystem : SystemComponent<CollectingViewComponent>
    {
        private ICameraService _cameraService;  
        private InventoryModel _inventoryModel;
        private LevelModel _levelModel;

        [Inject]
        private void Construct(ICameraService cameraService, InventoryModel inventoryModel, LevelModel levelModel)
        {
            _cameraService = cameraService;
            _inventoryModel = inventoryModel;
            _levelModel = levelModel;
        }

        protected override void OnLateUpdate()
        {
            base.OnLateUpdate();
            
            Components.Foreach(UpdatePosition);
        }

        protected override void OnEnableComponent(CollectingViewComponent armament)
        {
            base.OnEnableComponent(armament);

            SubscribeOnStartCollecting(armament);
            SubscribeOnCancelCollecting(armament);
        }
        
        protected override void OnDisableComponent(CollectingViewComponent component)
        {
            base.OnDisableComponent(component);
            
            component.Tween?.Kill(true);
        }

        private void SubscribeOnStartCollecting(CollectingViewComponent component)
        {
            void SetFillAmount(float value) => component.Fill.fillAmount = value;
            void SetCanvasGroupAlphaOne() => component.CanvasGroup.alpha = 1f;
            void SetCanvasGroupAlphaZero() => component.CanvasGroup.alpha = 0f;

            
            SetCanvasGroupAlphaZero();

            _inventoryModel.StartCollectingLoot
                .Subscribe(delay => {
                    component.Tween = DOVirtual.Float(0f, 1f, delay, SetFillAmount)
                        .SetEase(Ease.Linear)
                        .OnStart(SetCanvasGroupAlphaOne)
                        .OnComplete(SetCanvasGroupAlphaZero);
                })
                .AddTo(component.LifetimeDisposable);
        }
        
        private void SubscribeOnCancelCollecting(CollectingViewComponent component)
        {
            _inventoryModel.CancelCollectingLoot
                .Subscribe(_ => {
                    component.Tween.Kill(true);
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void UpdatePosition(CollectingViewComponent component)
        {
            Vector3 targetPosition = _levelModel.Character.Position.AddY(_levelModel.Character.Height);
            Vector3 targetScreenPosition = _cameraService.Camera.WorldToScreenPoint(targetPosition);
            
            component.Transform.position = targetScreenPosition;
        }
    }
}