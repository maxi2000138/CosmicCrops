using _Project.Scripts.Infrastructure.Systems.Components;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.Menu.Features.Shop.Components
{
  public class ShopCharacterRendererComponent : MonoComponent<ShopCharacterRendererComponent>,
    IBeginDragHandler, IDragHandler, IEndDragHandler
  {
    [SerializeField] private RawImage _rawImage;

    public RawImage RawImage => _rawImage;
    public ReactiveCommand<PointerEventData> OnTouch { get; } = new();
    public ReactiveCommand OnStartTouch { get; } = new();
    public ReactiveCommand OnEndTouch { get; } = new();
    public Tween Tween { get; set; }

    void IDragHandler.OnDrag(PointerEventData eventData) => OnTouch.Execute(eventData);
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) => OnStartTouch.Execute(Unit.Default);
    void IEndDragHandler.OnEndDrag(PointerEventData eventData) => OnEndTouch.Execute(Unit.Default);

  }
}