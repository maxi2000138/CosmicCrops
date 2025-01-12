using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.StaticData.Data;
using _Project.Scripts.UI.GUIService;
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
        private readonly IGuiService _guiService;
        private readonly IAssetService _assetService;
        private readonly IObjectResolver _objectResolver;

        public UIFactory(IStaticDataService staticDataService, IGuiService guiService, 
            IAssetService assetService, IObjectResolver objectResolver)
        {
            _staticDataService = staticDataService;
            _guiService = guiService;
            _assetService = assetService;
            _objectResolver = objectResolver;
        }

        async UniTask<BaseScreen> IUIFactory.CreateScreen(ScreenType type)
        {
            _guiService.Pop();
            ScreenData data = _staticDataService.ScreenData(type);
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            BaseScreen screen = _objectResolver.Instantiate(prefab, _guiService.StaticCanvas.transform).GetComponent<BaseScreen>();
            _guiService.Push(screen);
            return screen;
        }

        async UniTask<BaseScreen> IUIFactory.CreatePopUp(ScreenType type)
        {
            ScreenData data = _staticDataService.ScreenData(type);
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            BaseScreen screen = _objectResolver.Instantiate(prefab, _guiService.StaticCanvas.transform).GetComponent<BaseScreen>();
            _guiService.Push(screen);
            return screen;
        }
    }
}