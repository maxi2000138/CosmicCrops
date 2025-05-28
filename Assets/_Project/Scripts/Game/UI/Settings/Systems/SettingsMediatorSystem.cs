using _Project.Scripts.Game.UI.Settings.Components;
using _Project.Scripts.Infrastructure.Progress;
using _Project.Scripts.Infrastructure.Systems;
using R3;
using VContainer;

namespace _Project.Scripts.Game.UI.Settings.Systems
{
  public sealed class SettingsMediatorSystem : SystemComponent<SettingsMediatorComponent>
  {
    private IProgressService _progressService;

    [Inject]
    private void Construct(IProgressService progressService)
    {
      _progressService = progressService;
    }
        
    protected override void OnEnableComponent(SettingsMediatorComponent component)
    {
      base.OnEnableComponent(component);

      Init(component);
            
      component.VibrationToggle.IsEnable
        .Subscribe(SetHapticData)
        .AddTo(component.LifetimeDisposable);
    }

    private void Init(SettingsMediatorComponent component) =>
      component.VibrationToggle.IsEnable.Value = _progressService.HapticData.Data.Value;
        
    private void SetHapticData(bool isEnable) => _progressService.HapticData.Data.Value = isEnable;
  }
}