using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.GUI;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.UI._Configs;
using _Project.Scripts.UI.Screens;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.Factories.UI
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class UIFactory : IUIFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ScreensConfig _screensConfig;
        private readonly IGuiService _guiService;
        private readonly IAssetService _assetService;
        private readonly IObjectResolver _objectResolver;

        public UIFactory(ScreensConfig screensConfig, IGuiService guiService, 
            IAssetService assetService, IObjectResolver objectResolver)
        {
            _screensConfig = screensConfig;
            _guiService = guiService;
            _assetService = assetService;
            _objectResolver = objectResolver;
        }

        async UniTask<BaseScreen> IUIFactory.CreateScreen(ScreenType type)
        {
            _guiService.Pop();
            ScreenData data = _screensConfig.Data[type];
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.Prefab.Name);
            BaseScreen screen = _objectResolver.Instantiate(prefab, _guiService.StaticCanvas.transform).GetComponent<BaseScreen>();
            _guiService.Push(screen);
            return screen;
        }

        async UniTask<BaseScreen> IUIFactory.CreatePopUp(ScreenType type)
        {
            ScreenData data = _screensConfig.Data[type];
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.Prefab.Name);
            BaseScreen screen = _objectResolver.Instantiate(prefab, _guiService.StaticCanvas.transform).GetComponent<BaseScreen>();
            _guiService.Push(screen);
            return screen;
        }
    }
}