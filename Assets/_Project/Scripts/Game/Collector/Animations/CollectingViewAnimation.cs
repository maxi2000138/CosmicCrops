using _Project.Scripts.Game.Collector.Components;
using _Project.Scripts.Infrastructure.Animator;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Game.Collector.Animations
{
  public class CollectingViewAnimation : BaseAnimatorTween
  {
    [SerializeField] private CollectingViewComponent _component;
    public void StartReloading(float delay)
    {
      void SetFillAmount(float value) => _component.Fill.fillAmount = value;
      void SetCanvasGroupAlphaOne() => _component.CanvasGroup.alpha = 1f;

      var sequence = DOTween.Sequence();

      sequence.Append(DOVirtual.Float(0f, 1f, delay, SetFillAmount)
        .SetEase(Ease.Linear)
        .SetLink(_component.gameObject)
        .OnStart(SetCanvasGroupAlphaOne)
        .OnComplete(SetCanvasGroupAlphaZero));

      StartAnimation(sequence);
    }
    
    public void CancelReloading() => CancelAnimation();
    public void OnSubscribe() => SetCanvasGroupAlphaZero();

    
    private void SetCanvasGroupAlphaZero() => _component.CanvasGroup.alpha = 0f;
  }
}