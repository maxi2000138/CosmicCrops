using _Project.Scripts.Game.UI.Haptic.Components;
using _Project.Scripts.Infrastructure.Haptic;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils;
using _Project.Scripts.Utils.Constants;
using R3;
using VContainer;

namespace _Project.Scripts.Game.UI.Haptic.Systems
{
  public class HapticButtonSystem : SystemComponent<HapticButtonComponent>
  {
    private IHapticService _hapticService;

    [Inject]
    private void Construct(IHapticService hapticService)
    {
      _hapticService = hapticService;
    }
        
    protected override void OnEnableComponent(HapticButtonComponent component)
    {
      base.OnEnableComponent(component);

      component.Button
        .OnClickAsObservable()
        .ThrottleFirst(ButtonSettings.ClickThrottle)
        .Subscribe(_ => _hapticService.Play(component.HapticType))
        .AddTo(component.LifetimeDisposable);
    }
    }
}