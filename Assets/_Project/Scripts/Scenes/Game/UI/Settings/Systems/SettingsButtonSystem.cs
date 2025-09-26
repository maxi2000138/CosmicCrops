using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.UI.Screens;
using _Project.Scripts.Game.UI.Settings.Components;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.UI.Settings.Systems
{
    public sealed class SettingsButtonSystem : SystemComponent<SettingsButtonComponent>
    {
        private IUIFactory _uiFactory;
        private LevelModel _levelModel;

        [Inject]
        private void Construct(IUIFactory uiFactory, LevelModel levelModel)
        {
            _levelModel = levelModel;
            _uiFactory = uiFactory;
        }

        protected override void OnEnableComponent(SettingsButtonComponent component)
        {
            base.OnDisableComponent(component);
            
            component.Button
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    Components.Foreach(button => button.Button.interactable = false);

                    component.Tween = component.Button.transform
                        .DOLocalRotate(Vector3.back * 180f, 0.5f)
                        .SetRelative()
                        .OnComplete(CreatePopUp);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(SettingsButtonComponent component)
        {
            base.OnDisableComponent(component);
            
            component.Tween?.Kill();
        }

        private async UniTaskVoid CreateSettingsPopUp()
        {
            BaseScreen screen = await _uiFactory.CreatePopUp(ScreenType.Settings);

            _levelModel.OnPause.Execute(true);

            screen.CloseScreen
                .First()
                .Subscribe(CloseScreen)
                .AddTo(LifetimeDisposable);
        }

        private void CreatePopUp() => CreateSettingsPopUp().Forget();

        private void CloseScreen(Unit _)
        {
            _levelModel.OnPause.Execute(false);
            
            Components.Foreach(button => button.Button.interactable = true);
        }
    }
}