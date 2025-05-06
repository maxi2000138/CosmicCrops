using System;
using System.Text;
using System.Threading;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Logger;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Infrastructure.Curtain
{
    public sealed class LoadingCurtain : MonoBehaviour, ILoadingCurtainService
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private float _durationCanvas;
        [SerializeField] private float _durationPrintText;

        private readonly CancellationTokenSource _cancellationTokenSource = new ();
        private readonly StringBuilder _stringBuilder = new ();
        
        private IAssetDownloadReporter _assetDownloadReporter;

        [Inject]
        private void Construct(IAssetDownloadReporter assetDownloadReporter)
        {
            _assetDownloadReporter = assetDownloadReporter;
        }
        
        private void Awake()
        {
            _assetDownloadReporter.ProgressUpdated += OnProgressUpdated;
        }

        private void OnDestroy()
        {
            _assetDownloadReporter.ProgressUpdated -= OnProgressUpdated;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        void ILoadingCurtainService.Show()
        {
            gameObject.SetActive(true);
            
            _loadingText.text = string.Empty;
            _canvasGroup.alpha = 1f;
        }

        void ILoadingCurtainService.Hide()
        {
            ShowAnimation().Forget();
        }

        private async UniTaskVoid ShowAnimation()
        {
            try
            {
                int index = 0;
                float elapsed = 0f;

                while (index < 4)
                {
                    UpdateText(ref index);
                    
                    await UniTask.Delay(TimeSpan.FromSeconds(_durationPrintText), cancellationToken: _cancellationTokenSource.Token);
                }

                while (elapsed < _durationCanvas)
                {
                    elapsed += UnityEngine.Time.deltaTime;
                    _canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / _durationCanvas);
                    
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: _cancellationTokenSource.Token);
                }
                
                gameObject.SetActive(false);
            }
            catch (OperationCanceledException exception)
            {
                DebugLogger.Log(exception.Message, LogsType.Infrastructure);
            }
        }

        private void UpdateText(ref int index)
        {
            _stringBuilder.Clear();
            
            int dotCount = index % 4;
            
            for (int i = 0; i < dotCount; i++)
            {
                _stringBuilder.Append('.');
            }
            
            _loadingText.text = _stringBuilder.ToString();
            
            index++;
        }
        
        private void OnProgressUpdated()
        {
            if(_progressBar.Disabled) _progressBar.Enable();
            if(Mathf.Approximately(_assetDownloadReporter.Progress, 1f)) _progressBar.Disable();
            
            _progressBar.SetProgress(_assetDownloadReporter.Progress);
        }
    }
}