using _Project.Scripts.Infrastructure.Haptic.Engine;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Haptic.Components
{
  public class HapticButtonComponent : MonoComponent<HapticButtonComponent>
  {
    [SerializeField] private Button _button;
    [SerializeField] private HapticType _hapticType;

    public Button Button => _button;
    public HapticType HapticType => _hapticType;
  }
}