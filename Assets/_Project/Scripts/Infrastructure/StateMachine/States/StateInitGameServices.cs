using _Project.Scripts.Infrastructure.Camera;
using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Haptic;
using _Project.Scripts.Infrastructure.Input;
using _Project.Scripts.Infrastructure.Pool;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Infrastructure.Time;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class StateInitGameServices : IEnterState
  {
    private readonly ILoadingCurtainService _loadingCurtain;
    private readonly IJoystickService _joystickService;
    private readonly IHapticService _hapticService;
    private readonly IObjectPoolService _poolService;
    private readonly ICameraService _cameraService;
    private readonly ITimeService _time;


    public StateInitGameServices(IJoystickService joystickService, ICameraService cameraService, ILoadingCurtainService loadingCurtain,
      ITimeService time, IHapticService hapticService, IObjectPoolService poolService)
    {
      _joystickService = joystickService;
      _cameraService = cameraService;
      _loadingCurtain = loadingCurtain;
      _time = time;
      _hapticService = hapticService;
      _poolService = poolService;
    }


    async UniTask IEnterState.Enter(IGameStateMachine gameStateMachine)
    {
      _loadingCurtain.Show();
      
      InitJoystick();
      InitCameraService();
      InitHaptic();
      InitPool();
      
      gameStateMachine.Enter<StateLoadMenu>();
    }

    private void InitJoystick() => _joystickService.Init(_time);
    private void InitCameraService() => _cameraService.Init();
    private void InitHaptic() => _hapticService.Init();
    private void InitPool() => _poolService.Init();
  }
}