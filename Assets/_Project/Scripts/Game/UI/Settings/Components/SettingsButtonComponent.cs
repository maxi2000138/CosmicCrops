using _Project.Scripts.Infrastructure.Systems.Components;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Game.UI.Settings.Components
{
  public sealed class SettingsButtonComponent : MonoComponent<SettingsButtonComponent>
  {
    [SerializeField] private Button _button;

    public Button Button => _button;
    public Tween Tween { get; set; }
  }
}