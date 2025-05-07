using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.UI.PointerArrow.Components
{
  public class PointerArrowComponent : MonoComponent<PointerArrowComponent>
  {
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;

    public RectTransform RectTransform => _rectTransform;
    public CanvasGroup CanvasGroup => _canvasGroup;
    public Rect Rect { get; private set; }
    public IEnemy Target { get; private set; }
    public float Offset { get; private set; }
        
    public void SetTarget(IEnemy target) => Target = target;
    public void SetRectProvider(Rect rect) => Rect = rect;
    public void SetOffset(float offset) => Offset = offset;
  }
}