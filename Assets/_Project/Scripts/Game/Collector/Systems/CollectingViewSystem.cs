using _Project.Scripts.Game.Collector.Components;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Game.Level.Model;
using _Project.Scripts.Utils.Extensions;
using _Project.Scripts.Game.Inventory;
using VHierarchy.Libs;
using UnityEngine;
using VContainer;
using R3;

namespace _Project.Scripts.Game.Collector.Systems
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

        protected override void OnEnableComponent(CollectingViewComponent component)
        {
            base.OnEnableComponent(component);

            SubscribeOnStartCollecting(component);
            SubscribeOnCancelCollecting(component);
        }

        private void SubscribeOnStartCollecting(CollectingViewComponent component)
        {
            component.collectingAnimation.OnSubscribe();
            
            _inventoryModel.StartCollectingLoot
                .Subscribe(delay => component.collectingAnimation.StartReloading(delay))
                .AddTo(component.LifetimeDisposable);
        }
        
        private void SubscribeOnCancelCollecting(CollectingViewComponent component)
        {
            _inventoryModel.CancelCollectingLoot
                .Subscribe(_ => component.collectingAnimation.CancelReloading())
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