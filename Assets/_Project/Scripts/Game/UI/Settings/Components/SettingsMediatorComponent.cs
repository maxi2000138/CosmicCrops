using _Project.Scripts.Game.UI.Toggle.Components;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.UI.Settings.Components
{
  public sealed class SettingsMediatorComponent : MonoComponent<SettingsMediatorComponent>
  {
    [SerializeField] private ToggleComponent _vibrationToggle;

    public ToggleComponent VibrationToggle => _vibrationToggle;
  }
}