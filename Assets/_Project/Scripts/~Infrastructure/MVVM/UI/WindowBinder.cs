using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.MVVM.UI
{
    public abstract class WindowBinder<T> : MonoBehaviour, IWindowBinder where T : WindowViewModel
    {
        protected T ViewModel;

        public async UniTask Bind(WindowViewModel viewModel)
        {
            ViewModel = (T) viewModel;
            await OnBind(ViewModel, this.GetCancellationTokenOnDestroy());
        }

        public async UniTask Close()
        {
            await OnClose(this.GetCancellationTokenOnDestroy());
            Destroy(gameObject);
        }

        protected virtual UniTask OnBind(T viewModel, CancellationToken cancellationToken) { return UniTask.CompletedTask; }
        protected virtual UniTask OnClose(CancellationToken cancellationToken) { return UniTask.CompletedTask; }
    }
}