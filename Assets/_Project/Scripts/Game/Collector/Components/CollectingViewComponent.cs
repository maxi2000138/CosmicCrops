using _Project.Scripts.Game.Collector.Animations;
using _Project.Scripts.Infrastructure.Systems.Components;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.Game.Collector.Components
{
  public sealed class CollectingViewComponent : MonoComponent<CollectingViewComponent>
  {
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _fill;
    [SerializeField] private Transform _transform;

    public CanvasGroup CanvasGroup => _canvasGroup;
    public Image Fill => _fill;
    public Transform Transform => _transform;

    [FormerlySerializedAs("ReloadingAnimation")]
    public CollectingViewAnimation collectingAnimation;
  }
}