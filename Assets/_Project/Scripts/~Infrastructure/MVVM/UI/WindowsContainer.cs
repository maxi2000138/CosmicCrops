using System.Collections.Generic;
using _Project.Scripts.Infrastructure._Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.MVVM.UI
{
    public class WindowsContainer : MonoBehaviour
    {
        [SerializeField] private Transform _screensContainer;
        [SerializeField] private Transform _popupsContainer;
        
        private readonly Dictionary<WindowViewModel, IWindowBinder> _openedPopupBinders = new();
        private IWindowBinder _openedScreenBinder;
        
        public async UniTask OpenPopup(WindowViewModel viewModel)
        {
            var prefabPath = GetPrefabPath(viewModel);
            var prefab = Resources.Load<GameObject>(prefabPath);
            var createdPopup = Instantiate(prefab, _popupsContainer);
            var binder = createdPopup.GetComponent<IWindowBinder>();
            
            _openedPopupBinders.Add(viewModel, binder);
            await binder.Bind(viewModel);
        }

        public async UniTask ClosePopup(WindowViewModel popupViewModel)
        {
            var binder = _openedPopupBinders[popupViewModel];
         
            _openedPopupBinders.Remove(popupViewModel);
            await binder?.Close();
        }

        public async UniTask OpenScreen(WindowViewModel viewModel)
        {
            if (viewModel == null) return;
            
            _openedScreenBinder?.Close();
            
            var prefabPath = GetPrefabPath(viewModel);
            var prefab = Resources.Load<GameObject>(prefabPath);
            var createdScreen = Instantiate(prefab, _screensContainer);
            var binder = createdScreen.GetComponent<IWindowBinder>();
            
            _openedScreenBinder = binder;
            await binder.Bind(viewModel);
        }

        private static string GetPrefabPath(WindowViewModel viewModel)
        {
            return $"Prefabs/UI/{viewModel.Id}";
        }
    }
}