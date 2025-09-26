using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Game.Features.Units._Components.UI
{
  public class EnemyHealthViewComponent : MonoComponent<EnemyHealthViewComponent>
  {
    [SerializeField] private Image _fill;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CanvasGroup _canvasGroup;

    public Image Fill => _fill;
    public TextMeshProUGUI Text => _text;
    public CanvasGroup CanvasGroup => _canvasGroup;
    public ReactiveProperty<IEnemy> Enemy { get; } = new();
  }
}