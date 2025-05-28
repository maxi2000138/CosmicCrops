using _Project.Scripts.Infrastructure.Haptic;
using _Project.Scripts.Infrastructure.Progress;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;
using R3;

namespace _Project.Scripts.Game.UI.Haptic.Systems
{
  public class HapticActiveSystem : SystemBase
  {
      private IProgressService _progressService;
      private IHapticService _hapticService;

      [Inject]
      private void Construct(IProgressService progressService, IHapticService hapticService)
      {
        _progressService = progressService;
        _hapticService = hapticService;
      }
          
      protected override void OnEnableSystem()
      {
        base.OnEnableSystem();

        _progressService.HapticData.Data
          .Subscribe(_hapticService.IsEnable)
          .AddTo(LifetimeDisposable);
      }
  }
}