using _Project.Scripts.Infrastructure.Systems.Components;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Game.UI.Toggle.Components
{
  public sealed class HandleComponent : MonoComponent<HandleComponent>, IDragHandler, IEndDragHandler
  {
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private GameObject _imageOn;
    [SerializeField] private GameObject _imageOff;
        
    public float Width => _rectTransform.rect.width;
    public float X => transform.localPosition.x;

    public ReactiveCommand<PointerEventData> OnDrag { get; } = new ReactiveCommand<PointerEventData>();
    public ReactiveCommand<Unit> OnEndDrag { get; } = new ReactiveCommand();

    public void IsActive(bool isActive)
    {
      _imageOn.SetActive(isActive);
      _imageOff.SetActive(!isActive);
    }

    void IDragHandler.OnDrag(PointerEventData eventData) => OnDrag.Execute(eventData);
        
    void IEndDragHandler.OnEndDrag(PointerEventData eventData) => OnEndDrag.Execute(Unit.Default);
  }
}