using _Project.Scripts.Infrastructure.Systems.Components;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.Game.UI.Toggle.Components
{
  public sealed class ToggleComponent : MonoComponent<ToggleComponent>, IPointerClickHandler
  {
    [SerializeField] private HandleComponent _handle;

    [SerializeField] private RectTransform _container;
    [SerializeField] private Image _containerImage;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;
    [SerializeField] private float _offset;

    public HandleComponent Handle => _handle;
    public float Offset { get; private set; }
    public Tween Tween { get; set; }

    public ReactiveProperty<bool> IsEnable { get; } = new ReactiveProperty<bool>(true);
    public ReactiveCommand<Unit> OnClick { get; } = new ReactiveCommand();

    public void IsActive(bool isActive) => _containerImage.color = isActive ? _activeColor : _inactiveColor;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) => OnClick.Execute(Unit.Default);

    public override void OnComponentCreate()
    {
      base.OnComponentCreate();
      
      SetOffset();
    }
    
    private void SetOffset() => Offset = _container.rect.width / 2f - _handle.Width / 2f - _offset;
  }
}