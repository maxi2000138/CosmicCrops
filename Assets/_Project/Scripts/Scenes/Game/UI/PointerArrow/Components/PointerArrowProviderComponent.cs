using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.UI.PointerArrow.Components
{
  public class PointerArrowProviderComponent : MonoComponent<PointerArrowProviderComponent>
  {
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _offset;
        
    public Rect Rect => _rectTransform.rect;
    public float Offset => _offset;
  }
}