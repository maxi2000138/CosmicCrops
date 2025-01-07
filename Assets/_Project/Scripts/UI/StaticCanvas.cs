using UnityEngine;

namespace _Project.Scripts.UI
{
    public sealed class StaticCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;

        public Canvas Canvas => _canvas;
        public CanvasGroup CanvasGroup => _canvasGroup;
    }
}