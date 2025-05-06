using _Project.Scripts.Game.Entities._Components.UI;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.UI._Configs;
using _Project.Scripts.Game.UI.Screens;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.GUI;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Utils.Extensions;
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
        private readonly UIPrefabsConfig _uiPrefabsConfig;
        private readonly IGuiService _guiService;
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _objectResolver;

        public UIFactory(ScreensConfig screensConfig, UIPrefabsConfig uiPrefabsConfig, IGuiService guiService, 
            IAssetProvider assetProvider, IObjectResolver objectResolver)
        {
            _screensConfig = screensConfig;
            _uiPrefabsConfig = uiPrefabsConfig;
            _guiService = guiService;
            _assetProvider = assetProvider;
            _objectResolver = objectResolver;
        }

        async UniTask<BaseScreen> IUIFactory.CreateScreen(ScreenType type)
        {
            _guiService.Pop();
            ScreenData data = _screensConfig.Data[type];
            GameObject prefab = await _assetProvider.LoadFromAddressable<GameObject>(data.Prefab.Name);
            BaseScreen screen = _objectResolver.Instantiate(prefab, _guiService.StaticCanvas.transform).GetComponent<BaseScreen>();
            _guiService.Push(screen);
            return screen;
        }

        async UniTask<BaseScreen> IUIFactory.CreatePopUp(ScreenType type)
        {
            ScreenData data = _screensConfig.Data[type];
            GameObject prefab = await _assetProvider.LoadFromAddressable<GameObject>(data.Prefab.Name);
            BaseScreen screen = _objectResolver.Instantiate(prefab, _guiService.StaticCanvas.transform).GetComponent<BaseScreen>();
            _guiService.Push(screen);
            return screen;
        }
        
        async UniTask<EnemyHealthViewComponent> IUIFactory.CreateEnemyHealth(IEnemy enemy, Transform parent)
        {
            GameObject prefab = await _assetProvider.LoadFromAddressable<GameObject>(_uiPrefabsConfig.HealthViewPrefab.Name);
            EnemyHealthViewComponent enemyHealth = Object.Instantiate(prefab, parent).GetComponent<EnemyHealthViewComponent>();
            enemyHealth.Enemy.SetValueAndForceNotify(enemy);
            return enemyHealth;
        }
    }
}