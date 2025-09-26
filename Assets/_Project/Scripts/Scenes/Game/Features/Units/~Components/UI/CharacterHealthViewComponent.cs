using _Project.Scripts.Infrastructure.Systems.Components;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Game.Features.Units._Components.UI
{
  public class CharacterHealthViewComponent : MonoComponent<CharacterHealthViewComponent>
  {
    [SerializeField] private Image _fill;
    [SerializeField] private Image _fillLerp;
    [SerializeField] private TextMeshProUGUI _text;

    public Image Fill => _fill;
    public Image FillLerp => _fillLerp;
    public TextMeshProUGUI Text => _text;
    public Tween Tween { get; set; }

  }
}