using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using ObservableCollections;
using R3;
using Sirenix.Utilities;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.MVVM.UI
{
    public class UIRootBinder : MonoBehaviour
    {
        [SerializeField] private WindowsContainer _windowsContainer;
        
        private readonly CompositeDisposable _subscriptions = new();
        
        
        public async UniTask Bind(UIRootViewModel viewModel)
        {
            _subscriptions.Add(viewModel.OpenedScreen.Subscribe(newScreenViewModel =>
            {
                _windowsContainer.OpenScreen(newScreenViewModel);
            }));

            var openedWindows = viewModel.OpenedPopups.Select(x => _windowsContainer.OpenPopup(x));
            
            _subscriptions.Add(viewModel.OpenedPopups.ObserveAdd().Subscribe(e =>
            {
                _windowsContainer.OpenPopup(e.Value);
            }));

            _subscriptions.Add(viewModel.OpenedPopups.ObserveRemove().Subscribe(e =>
            {
                _windowsContainer.ClosePopup(e.Value);
            }));
            
            OnBind(viewModel);
            
            await UniTask.WhenAll(openedWindows);
        }

        protected virtual void OnBind(UIRootViewModel viewModel) { }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}