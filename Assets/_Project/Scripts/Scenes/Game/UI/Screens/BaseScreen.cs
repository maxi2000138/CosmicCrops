using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Game.UI.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _safeArea;
        [SerializeField] protected Button _button;
        
        public readonly ReactiveCommand CloseScreen = new ();
        
        protected readonly CompositeDisposable LifeTimeDisposable = new();

        protected virtual void OnEnable() => _safeArea.ApplySafeArea();
        protected virtual void OnDisable() => LifeTimeDisposable.Clear();

        public abstract ScreenType GetScreenType();

        protected virtual async UniTask Show()
        {
            SetCanvasEnable(false);
            await FadeCanvas(0f, 1f).AsyncWaitForCompletion().AsUniTask();
            SetCanvasEnable(true);
        }
        
        protected virtual async UniTask Hide()
        {
            SetCanvasEnable(false);
            await _button.transform.PunchTransform().AsyncWaitForCompletion().AsUniTask();
        }
        
        public void SetActive(bool isActive)
        {
            _canvasGroup.alpha = isActive ? 1f : 0f;
            _canvasGroup.interactable = isActive;
            _canvasGroup.blocksRaycasts = isActive;
        }

        protected Tween FadeCanvas(float from, float to)
        {
            return _canvasGroup
                .DOFade(to, 0.1f)
                .From(from)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
        }

        protected Tween BounceButton()
        {
            return _button.transform
                .DOScale(Vector3.one * 1.05f, 0.5f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
        }

        private void SetCanvasEnable(bool isEnable)
        {
            _canvasGroup.interactable = isEnable;
            _canvasGroup.blocksRaycasts = isEnable;
        }
    }
}